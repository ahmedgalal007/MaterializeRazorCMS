using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Database;
using AspnetCoreStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using static AspnetCoreStarter.Services.DynamicQueryService;

namespace AspnetCoreStarter.Services;

public class SettingsQueryService: ISettingsQueryService
{
  private readonly ApplicationSettingsDbContext _context;

  public SettingsQueryService(ApplicationSettingsDbContext context)
  {
    _context = context;
  }

/// <summary>
/// 
/// </summary>
/// <param name="filters"></param>
/// <param name="start"></param>
/// <param name="length"></param>
/// <param name="draw"></param>
/// <returns></returns>
  public async Task<object> GetFilteredEntitiesAsync<T>(PropertyFilter[] filters, Int32 start = 0, Int32 length = 10, Int32 draw = 0) where T : class
  {
    var dbSet = _context.Set<T>();

    // var filteredQuery = (IQueryable)genericWhereMethod.Invoke(null, new object[] { dbSet, lambda });
    IQueryable<object> filteredQuery = GenerateQueryFilters(dbSet, filters);

    // 4. Execute the query and return results
    int total = await dbSet.CountAsync();
    int filtered = await filteredQuery?.CountAsync();
    var results = await filteredQuery?.Skip(start).Take(length).ToListAsync();
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
  public static IQueryable<T> GenerateQueryFilters<T>(IQueryable<T> query, PropertyFilter[] filters)
  {
    IQueryable<T> result = query.AsQueryable();
    if (result != null)
    {
      foreach (var filter in filters)
      {
        result = GenerateQueryFilter(result!, filter)!;
      }
    }
    return result;
  }
  private static IQueryable<T>? GenerateQueryFilter<T>(IQueryable<T> query, PropertyFilter PF)
  {
    // 2. Build the dynamic filter expression
    /// var properties = entityType.GetProperties();
    ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
    Expression property = Expression.Property(parameter, PF.PropertyName);

    // Handle type conversion for the value
    object convertedPropertyValue = TryConvertPropertyValue(PF.PropertyName, PF.PropertyValue, property);
    ConstantExpression constant = Expression.Constant(convertedPropertyValue, property.Type);
    Expression predicateBody = GetBredicateBody(PF.ComparisonType, property, constant);
    var lambda = Expression.Lambda<Func<T, bool>>(predicateBody, parameter);
    return query.Where(lambda);
  }

  #region Helpers

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
 

  #endregion
}
