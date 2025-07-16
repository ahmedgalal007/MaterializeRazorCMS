using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Entities.Database;

public class DynamicDbContextConfig
{
  public int Id { get; set; } // A common ID property for all dynamic entities
  [NotMapped]                            // You might also have a Dictionary<string, object> to store dynamic properties
  public Dictionary<string, object> DynamicProperties { get; set; } = new Dictionary<string, object>();
}

