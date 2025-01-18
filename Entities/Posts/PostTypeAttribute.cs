namespace AspnetCoreStarter.Entities.Posts;

public class PostTypeAttribute : BaseEntity
{
  public  string Name { get; set; }
  public Guid TypeID { get; set; }
  public virtual Attribute Type { get; set; }

  //! Reference Post Fields
  public bool IsReference { get; set; } = false;
  public Guid? ReferenceId { get; set; }
  public PostType? Reference { get; set; }

  //! Validation Fields
  public int Max { get; set; } = 0;
  public int Min { get; set; } = 0;
  public string? Format { get; set; } = string.Empty;
  public string? Default { get; set; } = string.Empty;
  public bool IsNullable { get; set; } = false;
  public bool IsList { get; set; } = false;
  public bool IsUnique { get; set; } = false;
}
