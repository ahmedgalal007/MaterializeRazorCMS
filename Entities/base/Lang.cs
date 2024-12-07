using AspnetCoreStarter.Entities.Articles;
using Humanizer;

namespace AspnetCoreStarter.Entities
{
  public class Lang : BaseEntity<int>
  {
    public Lang() { }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; } = false;
    public bool IsRTL { get; set; } = false;
    public virtual List<Article> Articles { get; set; } 
  }
}
