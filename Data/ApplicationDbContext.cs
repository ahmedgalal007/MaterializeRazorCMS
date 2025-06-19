using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Entities.Articles;
using AspnetCoreStarter.Entities.Keywords;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Posts;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ApplicationSettingsDbContext settingsContext)
    : DbContext(options)
{
  private readonly ApplicationSettingsDbContext _settingsContext = settingsContext;
  [NotMapped]
  private readonly List<DynamicTableConfig> _dynamicTableConfigs = settingsContext.TablesConfig.Include(e => e.Properties).ToList();
  //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
  //    : base(options)
  //{
  //}

  public DbSet<User> User { get; set; } = default!;
  public DbSet<Article> Articles { get; set; } = default!;
  public DbSet<Language> Languages { get; set; } = default!;
  public DbSet<Entities.Posts.Attribute> Attributes { get; set; } = default!;
  public DbSet<PostType> PostTypes { get; set; } = default!;
  public DbSet<Keyword> Keywords { get; set; } = default!;
  public DbSet<Category> Categories { get; set; } = default!;
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    //OBS!: you can filter types within the assembly based on context name, usefull on multitenant solutions
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    foreach (var config in _dynamicTableConfigs)
    {
      // EntityTypeBuilder<BaseDynamicEntity> entityBuilder = modelBuilder.Entity<BaseDynamicEntity>();
      EntityTypeBuilder entityBuilder = modelBuilder.Entity(config.EntityName);

      // entityBuilder.HasBaseType(typeof(BaseDynamicEntity));
      // Configure the table name dynamically
      entityBuilder.ToTable(config.TableName);

      // Configure primary key
      var keyProperty = config.Properties.FirstOrDefault(p => p.IsKey);
      if (keyProperty != null)
      {
        Type propertyDotNetType = GetDotNetType(keyProperty.PropertyType);
        // If using a common Id property, you can set it directly
        // If the key itself is dynamic, you'd use shadow properties or a composite key
        entityBuilder.Property(propertyDotNetType, keyProperty.PropertyName)
            .HasColumnName(keyProperty.PropertyName) // Use the property name as column name
            .IsRequired(keyProperty.IsRequired);
        entityBuilder.HasKey(keyProperty.PropertyName);
      }
      else
      {
        Type propertyDotNetType = GetDotNetType("guid");
        entityBuilder.Property(propertyDotNetType, "Id")
            .HasColumnName("Id") // Use the property name as column name
            .IsRequired(true);
        entityBuilder.HasKey("Id"); // Default to Id if no dynamic key is specified
      }

      // Add dynamic properties as Shadow Properties or map to DynamicProperties dictionary
      foreach (var propConfig in config.Properties.Where(p => !p.IsKey))
      {
        Type propertyDotNetType = GetDotNetType(propConfig.PropertyType);

        // Option 1: Use Shadow Properties (recommended for EF Core)
        // entityBuilder.Property<object>(propConfig.PropertyName)
        entityBuilder.Property(propertyDotNetType, propConfig.PropertyName)
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
      "guid" => typeof(Guid),
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


//public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//  public ApplicationDbContext CreateDbContext(object[] args)
//  {
//    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//    optionsBuilder.UseSqlServer((string)args[0]);

//    return new ApplicationDbContext(optionsBuilder.Options, (List<DynamicTableConfig>)args[1]);
//  }
//}
