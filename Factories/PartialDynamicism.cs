// If not used remove EntityFrameWorkCore.InMemory Package

namespace AspnetCoreStarter.Factories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

// Represents a dynamically configured table
public class DynamicTableConfig
{
  public string EntityName { get; set; } // e.g., "Customer"
  public string TableName { get; set; }  // e.g., "AppCustomers"
  public List<DynamicPropertyConfig> Properties { get; set; } = new List<DynamicPropertyConfig>();
}

// Represents a dynamically configured property
public class DynamicPropertyConfig
{
  public string PropertyName { get; set; } // e.g., "FirstName"
  public string PropertyType { get; set; } // e.g., "string", "int", "DateTime"
  public bool IsKey { get; set; }
  public bool IsRequired { get; set; }
  // Add more configurations as needed (e.g., MaxLength, IsConcurrencyToken)
}

public class BaseDynamicEntity
{
  public int Id { get; set; } // A common ID property for all dynamic entities
                              // You might also have a Dictionary<string, object> to store dynamic properties
  public Dictionary<string, object> DynamicProperties { get; set; } = new Dictionary<string, object>();
}

public class DynamicSchemaDbContext : DbContext
{
  private readonly List<DynamicTableConfig> _dynamicTableConfigs;

  // A generic DbSet that will be configured for different tables
  // This is not truly dynamic DbSet generation, but dynamic *mapping*
  public DbSet<BaseDynamicEntity> DynamicEntities { get; set; } // This is just a placeholder, use Model.FindEntityType instead

  public DynamicSchemaDbContext(DbContextOptions<DynamicSchemaDbContext> options, List<DynamicTableConfig> dynamicTableConfigs)
      : base(options)
  {
    _dynamicTableConfigs = dynamicTableConfigs;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    foreach (var config in _dynamicTableConfigs)
    {
      EntityTypeBuilder<BaseDynamicEntity> entityBuilder = modelBuilder.Entity<BaseDynamicEntity>();

      // Configure the table name dynamically
      entityBuilder.ToTable(config.TableName);

      // Configure primary key
      var keyProperty = config.Properties.FirstOrDefault(p => p.IsKey);
      if (keyProperty != null)
      {
        // If using a common Id property, you can set it directly
        // If the key itself is dynamic, you'd use shadow properties or a composite key
        entityBuilder.HasKey(e => e.Id);
      }
      else
      {
        entityBuilder.HasKey(e => e.Id); // Default to Id if no dynamic key is specified
      }

      // Add dynamic properties as Shadow Properties or map to DynamicProperties dictionary
      foreach (var propConfig in config.Properties.Where(p => !p.IsKey))
      {
        Type propertyDotNetType = GetDotNetType(propConfig.PropertyType);

        // Option 1: Use Shadow Properties (recommended for EF Core)
        entityBuilder.Property<object>(propConfig.PropertyName)
            .HasColumnName(propConfig.PropertyName) // Use the property name as column name
            .IsRequired(propConfig.IsRequired); // Example of applying constraints

        // Option 2: Map to a JSON column or value converter if using a Dictionary<string, object>
        // This would be more complex and depend on your database's JSON capabilities
        // Example: entityBuilder.Property(e => e.DynamicProperties).HasConversion(...)

        // Example for actual properties on BaseDynamicEntity if they existed and were common
        // if (propConfig.PropertyName == "Name") entityBuilder.Property(e => e.Name);
      }
    }
  }

  private Type GetDotNetType(string propertyType)
  {
    return propertyType.ToLowerInvariant() switch
    {
      "string" => typeof(string),
      "int" => typeof(int),
      "decimal" => typeof(decimal),
      "datetime" => typeof(DateTime),
      "bool" => typeof(bool),
      _ => typeof(string), // Default or throw exception
    };
  }

  // Example of how you would query dynamically (using GetType() and shadow properties)
  public IQueryable<T> QueryDynamicEntity<T>(string entityName) where T : BaseDynamicEntity
  {
    // You would need to find the correct entity type that was configured
    // in OnModelCreating based on your entityName
    var entityType = this.Model.FindEntityType(typeof(BaseDynamicEntity)); // This needs refinement to find the *specific* dynamic entity mapping for 'entityName'
    if (entityType == null)
    {
      throw new InvalidOperationException($"No dynamic entity configuration found for '{entityName}'.");
    }

    // To query, you'd effectively be querying 'BaseDynamicEntity' and then filtering/selecting based on your dynamic logic
    // This is where it gets tricky because the actual 'entityName' is just a configuration.
    // You would typically use `Set<BaseDynamicEntity>()` and then access shadow properties or the DynamicProperties dictionary.

    return Set<T>(); // This will return a DbSet of BaseDynamicEntity
  }
}

public class ExampleUsage
{
  public static void Main(string[] args)
  {
    var dynamicConfigs = new List<DynamicTableConfig>
        {
            new DynamicTableConfig
            {
                EntityName = "Customer",
                TableName = "DynamicCustomers",
                Properties = new List<DynamicPropertyConfig>
                {
                    new DynamicPropertyConfig { PropertyName = "Id", PropertyType = "int", IsKey = true },
                    new DynamicPropertyConfig { PropertyName = "Name", PropertyType = "string", IsRequired = true },
                    new DynamicPropertyConfig { PropertyName = "Email", PropertyType = "string" }
                }
            },
            new DynamicTableConfig
            {
                EntityName = "Order",
                TableName = "DynamicOrders",
                Properties = new List<DynamicPropertyConfig>
                {
                    new DynamicPropertyConfig { PropertyName = "Id", PropertyType = "int", IsKey = true },
                    new DynamicPropertyConfig { PropertyName = "OrderDate", PropertyType = "datetime", IsRequired = true },
                    new DynamicPropertyConfig { PropertyName = "TotalAmount", PropertyType = "decimal" }
                }
            }
        };

    var options = new DbContextOptionsBuilder<DynamicSchemaDbContext>()
        .UseInMemoryDatabase(databaseName: "DynamicDb")
        .Options;

    using (var context = new DynamicSchemaDbContext(options, dynamicConfigs))
    {
      context.Database.EnsureCreated();

      // Interacting with shadow properties requires careful reflection or direct EF Core methods
      // Example: Adding a dynamic customer
      var customerEntry = context.Entry(new BaseDynamicEntity { Id = 1 });
      customerEntry.Property("Name").CurrentValue = "Alice";
      customerEntry.Property("Email").CurrentValue = "alice@example.com";
      context.Add(customerEntry.Entity);

      context.SaveChanges();

      Console.WriteLine("Added dynamic customer.");

      // Querying dynamic data (using shadow properties)
      var customer = context.Set<BaseDynamicEntity>()
                            .Where(e => EF.Property<string>(e, "Name") == "Alice")
                            .ToList();

      foreach (var c in customer)
      {
        Console.WriteLine($"Retrieved Dynamic Customer: Id={c.Id}, Name={EF.Property<string>(c, "Name")}, Email={EF.Property<string>(c, "Email")}");
      }
    }
  }
}
