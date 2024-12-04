using System;
using System.Runtime;
using System.Security.Cryptography;

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
  public virtual BaseLocalizedEntity ShallowCopy()
  {
    return (BaseLocalizedEntity)MemberwiseClone();
  }
  public BaseLocalizedEntity DeepCopy()
  {
    BaseLocalizedEntity other = (BaseLocalizedEntity)MemberwiseClone();
    other.Id = Id;
    return other;
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

  public virtual BaseLocalizedEntity<TID> ShallowCopy()
  {
    return (BaseLocalizedEntity<TID>)MemberwiseClone();
  }
  public virtual BaseLocalizedEntity<TID> DeepCopy()
  {
    BaseLocalizedEntity<TID> other = (BaseLocalizedEntity<TID>)MemberwiseClone();
    other.Id = Id;
    return other;
  }
}
