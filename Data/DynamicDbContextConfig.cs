using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Data;

// Represents a dynamically configured table
public class DynamicTableConfig
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string EntityName { get; set; } // e.g., "Customer"
  public string TableName { get; set; }  // e.g., "AppCustomers"
  // [NotMapped]
  public List<DynamicPropertyConfig> Properties { get; set; } = new List<DynamicPropertyConfig>();
}

// Represents a dynamically configured property
public class DynamicPropertyConfig
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string PropertyName { get; set; } // e.g., "FirstName"
  public string PropertyType { get; set; } // e.g., "string", "int", "DateTime"
  public bool IsKey { get; set; }=false;
  public bool IsRequired { get; set; }=false ;
  // Add more configurations as needed (e.g., MaxLength, IsConcurrencyToken)
}

public class BaseDynamicEntity
{
  public int Id { get; set; } // A common ID property for all dynamic entities
  [NotMapped]                            // You might also have a Dictionary<string, object> to store dynamic properties
  public Dictionary<string, object> DynamicProperties { get; set; } = new Dictionary<string, object>();
}

