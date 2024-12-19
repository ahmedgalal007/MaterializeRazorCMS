using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Entities.Articles;

public class ArticlePost : BaseEntity
{
  public int Order { get; set; }
  public DefaultIdType PostID { get; set; }
  public virtual Post Post { get; set; } = new Post();
}
