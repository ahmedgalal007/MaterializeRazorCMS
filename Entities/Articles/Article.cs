using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Entities.Articles;

public class Article : BaseEntity
{
  public Uri? ArticleUri { get; set; } 
  public Guid? CategoryID { get; set; }
  public virtual Category? Category { get; set; }
  public virtual List<Language> Languages { get; set; } = new List<Language>();
  public virtual List<ArticlePost> ArticlePosts { get; set; } = new List<ArticlePost>();

}
