using AspnetCoreStarter.Data;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AspnetCoreStarter.Services;

public class DynamicQueryService : IDynamicQueryService
{
  private readonly ApplicationDbContext _context;

  public DynamicQueryService(ApplicationDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Dynamically queries entities by entity name and applies a filter.
  /// </summary>
  /// <typeparam name="TEntity">The expected type of the entity (if known and you want a strongly-typed result).</typeparam>
  /// <param name="entityName">The name of the entity (e.g., "Product", "Category").</param>
  /// <param name="propertyName">The name of the property to filter on.</param>
  /// <param name="propertyValue">The value to filter by.</param>
  /// <param name="comparisonType">The type of comparison (e.g., "Equals", "Contains", "StartsWith").</param>
  /// <returns>A list of objects that match the criteria. These will be of the actual entity type, but returned as `object`.</returns>
  public async Task<object> GetFilteredEntitiesAsync(
      string entityName,
      PropertyFilter[] filters, int start = 0, int length = 10, int draw = 0)
  {
    // 1. Get the DbSet for the given entity name using reflection
    var dbSetProperty = GetEntityDbSetInfoByName(_context, entityName);
    if (dbSetProperty == null)
    {
      throw new ArgumentException($"No DbSet found for entity name: {entityName}");
    }

    var entityType = dbSetProperty.PropertyType.GenericTypeArguments[0];
    var dbSet = GetDbSetByEntityName(_context, entityType);


    // var filteredQuery = (IQueryable)genericWhereMethod.Invoke(null, new object[] { dbSet, lambda });
    var filteredQuery = GenerateQueryFilters(dbSet, entityType, entityName, filters);

    // 4. Execute the query and return results
    int total = await dbSet?.Cast<object>().CountAsync();
    int filtered = await filteredQuery?.Cast<object>().CountAsync();
    var results = await filteredQuery?.Cast<object>().Skip(start).Take(length).ToListAsync();
    return new
    {
      Draw = draw,
      RecordsTotal = total,
      RecordsFiltered = filtered,
      Data = results,
      Start = start,
      Length = length
    };
  }
  public struct PropertyFilter
  {
    public PropertyFilter(string propertyName, object propertyValue, string comparisonType = "Equals")
    {
      PropertyName = propertyName;
      PropertyValue = propertyValue;
      ComparisonType = comparisonType;
    }

    public string PropertyName { get; set; }
    public object PropertyValue { get; set; }
    public string ComparisonType { get; set; } = "Equals";
  }

  public static IQueryable? GenerateQueryFilters(IQueryable? query, Type entityType, string entityName, PropertyFilter[] filters)
  {
    IQueryable? result = query.AsQueryable();
    if (result != null)
    {
      foreach (var filter in filters)
      {
        result = GenerateQueryFilter(result!, entityType, entityName, filter);
      }
    }
    return result;
  }
  private static IQueryable? GenerateQueryFilter(IQueryable query, Type entityType, string entityName, PropertyFilter PF)
  {
    // 2. Build the dynamic filter expression
    ParameterExpression parameter = Expression.Parameter(entityType, "e");
    Expression property = Expression.Property(parameter, PF.PropertyName);

    // Handle type conversion for the value
    object convertedPropertyValue = TryConvertPropertyValue(entityName, PF.PropertyValue, property);

    ConstantExpression constant = Expression.Constant(convertedPropertyValue, property.Type);
    Expression predicateBody = GetBredicateBody(PF.ComparisonType, property, constant);

    var lambda = Expression.Lambda(predicateBody, parameter);

    // 3. Apply the filter using `Where` method via reflection
    var genericWhereMethod = GetGenericWhereMethod(entityType);
    return (IQueryable?)genericWhereMethod.Invoke(null, new object[] { query, lambda });
  }

  #region Helpers
  public static PropertyInfo? GetEntityDbSetInfoByName(DbContext context, string entityName)
  {
    return context.GetType().GetProperties()
       .FirstOrDefault(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                            p.PropertyType.GenericTypeArguments[0].Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

  }

  public static IQueryable? GetDbSetByEntityName(DbContext context, Type entityType)
  {
    return (IQueryable?)context.GetType().GetMethod("Set", 1, Type.EmptyTypes)
        .MakeGenericMethod(entityType)
        .Invoke(context, null);
  }

  private static object TryConvertPropertyValue(string propertyName, object propertyValue, Expression property)
  {
    if (property.Type != propertyValue.GetType())
    {
      try
      {
        return Convert.ChangeType(propertyValue, property.Type);
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Cannot convert '{propertyValue}' to type '{property.Type.Name}' for property '{propertyName}'.", ex);
      }
    }
    return propertyValue;
  }


  private static Expression GetBredicateBody(string comparisonType, Expression property, ConstantExpression constant)
  {
    switch (comparisonType.ToLower())
    {
      case "equals":
        return Expression.Equal(property, constant);
        break;
      case "contains":
        if (property.Type != typeof(string))
          throw new ArgumentException("Contains comparison is only supported for string properties.");
        return Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant);
        break;
      case "startswith":
        if (property.Type != typeof(string))
          throw new ArgumentException("StartsWith comparison is only supported for string properties.");
        return Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant);
        break;
      case "endswith":
        if (property.Type != typeof(string))
          throw new ArgumentException("EndsWith comparison is only supported for string properties.");
        return Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant);
        break;
      case "greaterthan":
        return Expression.GreaterThan(property, constant);
        break;
      case "lessthan":
        return Expression.LessThan(property, constant);
        break;
      // Add more comparison types as needed (e.g., GreaterThanOrEqual, LessThanOrEqual, NotEqual)
      default:
        throw new ArgumentException($"Unsupported comparison type: {comparisonType}");
    }
  }


  private static MethodInfo GetGenericWhereMethod(Type? entityType)
  {
    var whereMethod = typeof(Queryable).GetMethods()
        .Where(m => m.Name == "Where" && m.IsGenericMethodDefinition)
        .Select(m => new { Method = m, Params = m.GetParameters() })
        .Where(p => p.Params.Length == 2 && p.Params[1].ParameterType.Name == "Expression`1")
        .Select(p => p.Method)
        .First();
    return whereMethod.MakeGenericMethod(entityType); ;
  }

  #endregion
}
