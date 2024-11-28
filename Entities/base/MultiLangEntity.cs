using System.Security.Cryptography;

namespace AspnetCoreStarter.Entities;

public abstract class MultiLangEntity<T, TID> : BaseEntity<TID>
  where T : BaseLocalizedEntity, new()
{
  protected MultiLangEntity() {
    Locales = new List<T>();
  }
  public MultiLangEntity(Lang defaultlanguage) 
  {
    Locales = new List<T>();
  }
  public virtual List<T> Locales { get; set; }
  public abstract T AddLocal(T local);
  public abstract T RemoveLocal(T local);
  public abstract T UpdateLocal(T local);
}
