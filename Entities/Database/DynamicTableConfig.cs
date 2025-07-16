namespace AspnetCoreStarter.Entities.Database;

// Represents a dynamically configured table
public class DynamicTableConfig
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string EntityName { get; set; } // e.g., "Customer"
  public string TableName { get; set; }  // e.g., "AppCustomers"
  // [NotMapped]
  public List<DynamicPropertyConfig> Properties { get; set; } = new List<DynamicPropertyConfig>();
}

