using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspnetCoreStarter.Data;

public static class DynamicContextExtensions
{
  public static IQueryable Query(this DbContext context, string entityName) =>
        context.Query(context.Model.FindEntityType(entityName).ClrType);

  static readonly MethodInfo SetMethod =
      typeof(DbContext).GetMethod(nameof(DbContext.Set), 1, Array.Empty<Type>()) ??
      throw new Exception($"Type not found: DbContext.Set");

  public static IQueryable Query(this DbContext context, Type entityType) =>
      (IQueryable)SetMethod.MakeGenericMethod(entityType)?.Invoke(context, null) ??
      throw new Exception($"Type not found: {entityType.FullName}");

  /// <summary>
  /// Generates an IQueryable for a given entity name dynamically.
  /// </summary>
  /// <param name="context">The DbContext instance.</param>
  /// <param name="entityName">The name of the entity (e.g., "Product" or "MyNamespace.Product").</param>
  /// <returns>An IQueryable for the specified entity name, or null if the entity type is not found or not tracked.</returns>
  public static IQueryable GetQueryable(this DbContext context, string entityName)
  {
    if (string.IsNullOrWhiteSpace(entityName))
    {
      throw new ArgumentException("Entity name cannot be null or empty.", nameof(entityName));
    }

    // Find the EntityType from the DbContext's model
    var entityType = context.Model.FindEntityType(entityName)?.ClrType;

    if (entityType == null)
    {
      // You might want to log this or throw a more specific exception
      Console.WriteLine($"Warning: Entity type '{entityName}' not found in the DbContext model.");
      return null;
    }

    return context.Query(entityType);
  }
}
