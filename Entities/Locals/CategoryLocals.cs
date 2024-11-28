namespace AspnetCoreStarter.Entities.Locals;

public class CategoryLocals : BaseLocalizedEntity
{
  public CategoryLocals()
  {
  }
  public CategoryLocals(Lang lang) : base(lang)
  {
    
  }
  public string Name { get; set; } = string.Empty;
}
