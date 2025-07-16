using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStarter.Entities.Database;

// Represents a dynamically configured property
public class DynamicPropertyConfig
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

  public int ColSize { get; set; } = 12;
  public int Index { get; set; } = 0;
  public int ResponsivePriority { get; set; } = 0;
  public string ControlName { get; set; } // e.g., "FirstName"
  public string PropertyName { get; set; } // e.g., "FirstName"
  public string PropertyType { get; set; } // e.g., "string", "int", "DateTime"
  public bool IsKey { get; set; }=false;
  public bool IsRequired { get; set; }=false ;
  // Add more configurations as needed (e.g., MaxLength, IsConcurrencyToken)
}

