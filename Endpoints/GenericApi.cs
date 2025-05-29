using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DynamicCrudApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DynamicCrudController : ControllerBase
  {
    private readonly YourApplicationDbContext _context; // Replace with your actual DbContext

    public DynamicCrudController(YourApplicationDbContext context)
    {
      _context = context;
    }

    // Helper to get the Type of an entity by its name
    private Type GetEntityType(string entityName)
    {
      // In a real application, you might use an IoC container,
      // a dictionary of known entity types, or scan assemblies.
      // For simplicity, this example assumes entities are in the same assembly
      // as the DbContext and have the same namespace or are directly accessible.

      // Option 1: Scan current assembly (less efficient, but general)
      var entityType = Assembly.GetExecutingAssembly()
                               .GetTypes()
                               .FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

      // Option 2: Look within the DbContext's assembly (more targeted if all entities are in DbContext's assembly)
      if (entityType == null)
      {
        entityType = _context.GetType().Assembly
                             .GetTypes()
                             .FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
      }

      // Option 3: A predefined dictionary (most efficient if entities are known)
      // private static readonly Dictionary<string, Type> _entityTypes = new Dictionary<string, Type>
      // {
      //     { "Product", typeof(Product) },
      //     { "Order", typeof(Order) }
      // };
      // _entityTypes.TryGetValue(entityName, out Type entityType);

      return entityType;
    }

    // Helper to get the DbSet<TEntity> from DbContext using reflection
    private object GetDbSet(Type entityType)
    {
      try
      {
        var dbSetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
        var genericDbSetMethod = dbSetMethod.MakeGenericMethod(entityType);
        return genericDbSetMethod.Invoke(_context, null);
      }
      catch (Exception ex)
      {
        // Log or handle the exception appropriately
        Console.WriteLine($"Error getting DbSet for {entityType.Name}: {ex.Message}");
        return null;
      }
    }

    // GET: api/DynamicCrud/Product
    // GET: api/DynamicCrud/Order
    [HttpGet("{entityName}")]
    public async Task<IActionResult> GetAll(string entityName)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null)
      {
        return NotFound($"Entity '{entityName}' not found.");
      }

      var dbSet = GetDbSet(entityType);
      if (dbSet == null)
      {
        return StatusCode(500, $"Could not access DbSet for entity '{entityName}'.");
      }

      try
      {
        // Get the ToListAsync method via reflection
        var toListMethod = typeof(EntityFrameworkQueryableExtensions).GetMethod(nameof(EntityFrameworkQueryableExtensions.ToListAsync), BindingFlags.Static | BindingFlags.Public);
        var genericToListAsyncMethod = toListMethod.MakeGenericMethod(entityType);

        // Invoke ToListAsync on the DbSet
        var task = (Task)genericToListAsyncMethod.Invoke(null, new object[] { dbSet });
        await task;

        // Get the result using dynamic
        dynamic result = task.GetType().GetProperty("Result").GetValue(task, null);

        return Ok(result);
      }
      catch (TargetInvocationException tie)
      {
        return StatusCode(500, $"An error occurred while retrieving entities: {tie.InnerException?.Message ?? tie.Message}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
      }
    }

    // GET: api/DynamicCrud/Product/5
    // GET: api/DynamicCrud/Order/guid-id
    [HttpGet("{entityName}/{id}")]
    public async Task<IActionResult> GetById(string entityName, string id)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null)
      {
        return NotFound($"Entity '{entityName}' not found.");
      }

      var dbSet = GetDbSet(entityType);
      if (dbSet == null)
      {
        return StatusCode(500, $"Could not access DbSet for entity '{entityName}'.");
      }

      try
      {
        // Find the primary key property (assuming "Id" or "EntityNameId")
        var pkProperty = entityType.GetProperty("Id") ?? entityType.GetProperty($"{entityName}Id");
        if (pkProperty == null)
        {
          return BadRequest($"Could not determine primary key for entity '{entityName}'.");
        }

        // Convert the ID to the primary key's type
        object convertedId;
        try
        {
          convertedId = Convert.ChangeType(id, pkProperty.PropertyType);
        }
        catch (FormatException)
        {
          return BadRequest($"Invalid ID format for entity '{entityName}'. Expected type: {pkProperty.PropertyType.Name}");
        }

        // Get the FindAsync method via reflection
        var findAsyncMethod = typeof(DbContext).GetMethod(nameof(DbContext.FindAsync), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object[]) }, null);
        if (findAsyncMethod == null)
        {
          return StatusCode(500, "FindAsync method not found on DbContext.");
        }

        var genericFindAsyncMethod = findAsyncMethod.MakeGenericMethod(entityType);

        // Invoke FindAsync
        var task = (Task)genericFindAsyncMethod.Invoke(_context, new object[] { new object[] { convertedId } });
        await task;

        // Get the result using dynamic
        dynamic entity = task.GetType().GetProperty("Result").GetValue(task, null);

        if (entity == null)
        {
          return NotFound();
        }

        return Ok(entity);
      }
      catch (TargetInvocationException tie)
      {
        return StatusCode(500, $"An error occurred while retrieving entity: {tie.InnerException?.Message ?? tie.Message}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
      }
    }

    // POST: api/DynamicCrud/Product
    [HttpPost("{entityName}")]
    public async Task<IActionResult> Create(string entityName, [FromBody] object entityData)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null)
      {
        return NotFound($"Entity '{entityName}' not found.");
      }

      var dbSet = GetDbSet(entityType);
      if (dbSet == null)
      {
        return StatusCode(500, $"Could not access DbSet for entity '{entityName}'.");
      }

      try
      {
        // Deserialize the JSON data into a concrete type
        // This is the trickiest part. You need a way to map 'entityData' (which is just a JObject/Dictionary)
        // to a concrete instance of 'entityType'. Newtonsoft.Json or System.Text.Json can help.
        // For simplicity, let's assume entityData is a dynamic object that can be mapped directly.
        // In a real scenario, you'd use a JSON deserializer.

        // Example using System.Text.Json (requires NuGet package System.Text.Json)
        var jsonString = System.Text.Json.JsonSerializer.Serialize(entityData);
        dynamic newEntity = System.Text.Json.JsonSerializer.Deserialize(jsonString, entityType);

        if (newEntity == null)
        {
          return BadRequest("Invalid entity data provided.");
        }

        // Get the Add method via reflection
        var addMethod = typeof(DbSet<>).MakeGenericType(entityType).GetMethod(nameof(DbSet<object>.Add));
        addMethod.Invoke(dbSet, new object[] { newEntity });

        await _context.SaveChangesAsync();

        // Get the ID of the newly created entity (assuming "Id" property)
        var pkProperty = entityType.GetProperty("Id") ?? entityType.GetProperty($"{entityName}Id");
        object newId = pkProperty?.GetValue(newEntity);

        // Return CreatedAtAction with the ID
        return CreatedAtAction(nameof(GetById), new { entityName = entityName, id = newId?.ToString() }, newEntity);
      }
      catch (TargetInvocationException tie)
      {
        return StatusCode(500, $"An error occurred while creating entity: {tie.InnerException?.Message ?? tie.Message}");
      }
      catch (System.Text.Json.JsonException jex)
      {
        return BadRequest($"Invalid JSON format: {jex.Message}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
      }
    }

    // PUT: api/DynamicCrud/Product/5
    [HttpPut("{entityName}/{id}")]
    public async Task<IActionResult> Update(string entityName, string id, [FromBody] object entityData)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null)
      {
        return NotFound($"Entity '{entityName}' not found.");
      }

      var dbSet = GetDbSet(entityType);
      if (dbSet == null)
      {
        return StatusCode(500, $"Could not access DbSet for entity '{entityName}'.");
      }

      try
      {
        // Find the primary key property
        var pkProperty = entityType.GetProperty("Id") ?? entityType.GetProperty($"{entityName}Id");
        if (pkProperty == null)
        {
          return BadRequest($"Could not determine primary key for entity '{entityName}'.");
        }

        // Convert the ID to the primary key's type
        object convertedId;
        try
        {
          convertedId = Convert.ChangeType(id, pkProperty.PropertyType);
        }
        catch (FormatException)
        {
          return BadRequest($"Invalid ID format for entity '{entityName}'. Expected type: {pkProperty.PropertyType.Name}");
        }

        // Get the existing entity
        var findAsyncMethod = typeof(DbContext).GetMethod(nameof(DbContext.FindAsync), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object[]) }, null);
        var genericFindAsyncMethod = findAsyncMethod.MakeGenericMethod(entityType);
        var task = (Task)genericFindAsyncMethod.Invoke(_context, new object[] { new object[] { convertedId } });
        await task;
        dynamic existingEntity = task.GetType().GetProperty("Result").GetValue(task, null);

        if (existingEntity == null)
        {
          return NotFound();
        }

        // Update properties using reflection or a tool like AutoMapper if entityData is structured.
        // For simplicity, let's again assume entityData is a dynamic object that can be mapped.
        // A more robust solution would involve deserializing entityData to a temporary object
        // and then copying properties.

        var jsonString = System.Text.Json.JsonSerializer.Serialize(entityData);
        dynamic updatedData = System.Text.Json.JsonSerializer.Deserialize(jsonString, entityType);

        if (updatedData == null)
        {
          return BadRequest("Invalid entity data provided for update.");
        }

        // Copy properties from updatedData to existingEntity
        foreach (PropertyInfo property in entityType.GetProperties())
        {
          // Skip the primary key property to avoid overwriting it
          if (property == pkProperty) continue;

          if (property.CanWrite)
          {
            var updatedValue = property.GetValue(updatedData);
            property.SetValue(existingEntity, updatedValue);
          }
        }

        _context.Entry(existingEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (DbUpdateConcurrencyException)
      {
        // Handle concurrency conflicts if needed
        if (!await EntityExists(entityName, id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      catch (TargetInvocationException tie)
      {
        return StatusCode(500, $"An error occurred while updating entity: {tie.InnerException?.Message ?? tie.Message}");
      }
      catch (System.Text.Json.JsonException jex)
      {
        return BadRequest($"Invalid JSON format: {jex.Message}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
      }
    }

    // DELETE: api/DynamicCrud/Product/5
    [HttpDelete("{entityName}/{id}")]
    public async Task<IActionResult> Delete(string entityName, string id)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null)
      {
        return NotFound($"Entity '{entityName}' not found.");
      }

      var dbSet = GetDbSet(entityType);
      if (dbSet == null)
      {
        return StatusCode(500, $"Could not access DbSet for entity '{entityName}'.");
      }

      try
      {
        // Find the primary key property
        var pkProperty = entityType.GetProperty("Id") ?? entityType.GetProperty($"{entityName}Id");
        if (pkProperty == null)
        {
          return BadRequest($"Could not determine primary key for entity '{entityName}'.");
        }

        // Convert the ID to the primary key's type
        object convertedId;
        try
        {
          convertedId = Convert.ChangeType(id, pkProperty.PropertyType);
        }
        catch (FormatException)
        {
          return BadRequest($"Invalid ID format for entity '{entityName}'. Expected type: {pkProperty.PropertyType.Name}");
        }

        // Get the existing entity
        var findAsyncMethod = typeof(DbContext).GetMethod(nameof(DbContext.FindAsync), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object[]) }, null);
        var genericFindAsyncMethod = findAsyncMethod.MakeGenericMethod(entityType);
        var task = (Task)genericFindAsyncMethod.Invoke(_context, new object[] { new object[] { convertedId } });
        await task;
        dynamic entityToDelete = task.GetType().GetProperty("Result").GetValue(task, null);

        if (entityToDelete == null)
        {
          return NotFound();
        }

        // Get the Remove method via reflection
        var removeMethod = typeof(DbSet<>).MakeGenericType(entityType).GetMethod(nameof(DbSet<object>.Remove));
        removeMethod.Invoke(dbSet, new object[] { entityToDelete });

        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (TargetInvocationException tie)
      {
        return StatusCode(500, $"An error occurred while deleting entity: {tie.InnerException?.Message ?? tie.Message}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
      }
    }

    // Helper to check if an entity exists (for concurrency handling)
    private async Task<bool> EntityExists(string entityName, string id)
    {
      var entityType = GetEntityType(entityName);
      if (entityType == null) return false;

      var dbSet = GetDbSet(entityType);
      if (dbSet == null) return false;

      var pkProperty = entityType.GetProperty("Id") ?? entityType.GetProperty($"{entityName}Id");
      if (pkProperty == null) return false;

      object convertedId;
      try
      {
        convertedId = Convert.ChangeType(id, pkProperty.PropertyType);
      }
      catch (FormatException)
      {
        return false;
      }

      var findAsyncMethod = typeof(DbContext).GetMethod(nameof(DbContext.FindAsync), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object[]) }, null);
      var genericFindAsyncMethod = findAsyncMethod.MakeGenericMethod(entityType);
      var task = (Task)genericFindAsyncMethod.Invoke(_context, new object[] { new object[] { convertedId } });
      await task;
      dynamic entity = task.GetType().GetProperty("Result").GetValue(task, null);

      return entity != null;
    }
  }

  // --- Example of your DbContext and Entity classes ---
  // You would replace 'YourApplicationDbContext' and these example entities
  // with your actual application's data model.

  public class YourApplicationDbContext : DbContext
  {
    public YourApplicationDbContext(DbContextOptions<YourApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    // Add more DbSets for your other entities
  }

  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
  }

  public class Order
  {
    public Guid Id { get; set; } // Example with Guid primary key
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
  }

  public class Customer
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }
}
