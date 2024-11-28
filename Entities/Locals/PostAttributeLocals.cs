namespace AspnetCoreStarter.Entities.Locals;

public class PostAttributeLocals : BaseLocalizedEntity
{
  public PostAttributeLocals()
  {
  }
  public PostAttributeLocals(Lang lang) : base(lang)
  {

  }


  public string ValueText { get; set; } = string.Empty;

}
