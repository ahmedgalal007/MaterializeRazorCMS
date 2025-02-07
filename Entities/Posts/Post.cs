namespace AspnetCoreStarter.Entities.Posts;

public class Post : BaseEntity
{
  public  Uri? PostURI { get; set; }
  public  DefaultIdType PostTypeID { get; set; }
  public  virtual PostType PostType { get; set; } = new PostType();
  public  List<PostAttribute> Attributes { get; set; } = new List<PostAttribute>();
}
