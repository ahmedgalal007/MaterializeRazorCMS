namespace AspnetCoreStarter.Entities.Locals;

public class KeywordLocals : BaseLocalizedEntity
{
  public KeywordLocals()
  {
  }
  public KeywordLocals(Language lang) : base(lang)
  {

  }
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}
