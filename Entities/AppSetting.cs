// Models/AppSetting.cs

namespace AspnetCoreStarter.Entities;
public class AppSetting
{
  public int Id { get; set; } // Primary Key
  public string Key { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty; // Optional: for documentation
  public DateTime LastModified { get; set; }
}
