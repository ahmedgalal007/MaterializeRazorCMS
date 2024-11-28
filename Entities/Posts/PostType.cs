namespace AspnetCoreStarter.Entities.Posts;

public class PostType : BaseEntity
{
  public  string Name { get; set; }
  public List<PostTypeAttribute> Attributes { get; set; } = new List<PostTypeAttribute>();
}
