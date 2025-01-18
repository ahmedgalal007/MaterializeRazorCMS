using AspnetCoreStarter.Entities.Categories;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Entities.Posts;

public class PostType : BaseEntity
{
  public string Name { get; set; }
  public string Slug { get; set; } = string.Empty;

  public Guid? ParentId { get; set; }
  public virtual PostType? Parent { get; set; }

  [ForeignKey(nameof(ParentId))]
  public virtual List<PostType> Childrens { get; set; } = new();
  public virtual List<PostTypeAttribute> Attributes { get; set; } = new List<PostTypeAttribute>();
}
