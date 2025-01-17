namespace AspnetCoreStarter.Entities.Posts;

public class Attribute : BaseEntity
{
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string BaseType { get; set; } = typeof(string).Name;

  /// <summary>
  /// Return Type (String || Number || Date || Boolean )
  /// </summary>
  public string ReturnType { get; set; } = typeof(string).Name;

  public int Max { get; set; }
  public int Min { get; set; }
  public string? Format { get; set; } = string.Empty;

  public virtual List<PostTypeAttribute> PostTypeAttributes { get; set; } = new List<PostTypeAttribute>();
}
