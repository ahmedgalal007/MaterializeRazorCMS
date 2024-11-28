namespace AspnetCoreStarter.Entities;

public class BaseLocalizedEntity : BaseLocalizedEntity<Guid>
{
  protected BaseLocalizedEntity()
  {
  }
  public BaseLocalizedEntity(Lang lang)
  {
    Language = lang;
  }

}
public class BaseLocalizedEntity<TID> : BaseEntity<TID>
{
  protected BaseLocalizedEntity()
  {
  }
  public BaseLocalizedEntity(Lang lang)
  {
    Language = lang;
  }
  public Lang Language { get; set; }
}
