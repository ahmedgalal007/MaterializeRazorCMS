using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Entities.Articles;

public class ArticlePost : BaseEntity
{
  public int Order { get; set; }
  public Post Post { get; set; }
}
