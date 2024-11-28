using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Posts;

namespace AspnetCoreStarter.Entities.Articles;

public class Article : BaseEntity
{
  public Uri? ArticleUri { get; set; } 
  public Category? Category { get; set; }
  public List<Lang> Languages { get; set; } = new List<Lang>();
  public List<ArticlePost> ArticlePosts { get; set; } = new List<ArticlePost>();

}
