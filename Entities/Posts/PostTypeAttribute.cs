namespace AspnetCoreStarter.Entities.Posts;

public class PostTypeAttribute : BaseEntity
{
  public  string Name { get; set; }
  public Guid TypeID { get; set; }
  public virtual Attribute Type { get; set; }
  public int Max { get; set; }
  public int Min { get; set; }
  public string Format { get; set; } = string.Empty;
  public bool IsNullable { get; set; } = false;
  public bool IsList { get; set; } = false;
  public bool IsUnique { get; set; } = false;
}
