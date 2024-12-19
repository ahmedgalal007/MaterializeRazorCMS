namespace AspnetCoreStarter.Entities.Locals;

public class PostAttributeLocals : BaseLocalizedEntity
{
  public PostAttributeLocals()
  {
  }
  public PostAttributeLocals(Language lang) : base(lang)
  {

  }


  public string ValueText { get; set; } = string.Empty;

}
