using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AspnetCoreStarter.Entities;

public abstract class MultiLangEntity<T, TID> : BaseEntity<TID>
  where T : BaseLocalizedEntity, new()
{
  protected MultiLangEntity() {
    Locales = new List<T>();
  }
  public MultiLangEntity(Language defaultlanguage) 
  {
    Locales = new List<T>();
  }
  public virtual List<T> Locales { get; set; }
  public abstract T AddLocal(T local);
  public abstract T RemoveLocal(T local);
  public abstract T UpdateLocal(T local);
  public T? GetLocal(string langCode)
  {
    return Locales.FirstOrDefault(x => x.LanguageID == langCode) ?? default(T);
  }

  public bool QueryLocales(string prop, string q)
  {
    return Locales.Where(x => EF.Property<string>(x , prop).Contains(q) ).Count() > 0;
  }
}
