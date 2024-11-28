namespace AspnetCoreStarter.Entities.Categories;

public class KeywordLocals : BaseLocalizedEntity
{
  public KeywordLocals()
  {
  }
  public KeywordLocals(Lang lang) : base(lang)
  {

  }
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}
