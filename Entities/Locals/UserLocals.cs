namespace AspnetCoreStarter.Entities.Locals;

public class UserLocals : BaseLocalizedEntity
{
  public UserLocals()
  {
  }
  public UserLocals(Language lang) : base(lang)
  {
    
  }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
}
