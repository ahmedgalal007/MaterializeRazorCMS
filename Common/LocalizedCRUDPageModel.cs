using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities;
using System;
using System.Reflection;

namespace AspnetCoreStarter.Common;

public class LocalizedCRUDPageModel<T, TLocal, TID> : CRUDPageModel<T, TID>
  where T : MultiLangEntity<TLocal, TID>, new()
  where TLocal : BaseLocalizedEntity, new()
  where TID : struct
{

  [BindProperty]
  public List<TLocal> Locals { get; set; }

  public List<string> GetLocalProperties => ExtractPropertiesNames();

  public LocalizedCRUDPageModel(ApplicationDbContext context) : base(context)
  {
  }

  protected async Task UpdateLocals()
  {
    Locals.ForEach(x => {
      
      TLocal? local = NewEntry.Locales.FirstOrDefault(C => C.Language.Code == x.Language.Code);
      if (local == null)
      {
        NewEntry.Locales.Add(x);
      }
      else
      {
        local = (TLocal)x.ShallowCopy();
      }
    });
    await Task.CompletedTask;
  }
  private List<string> ExtractPropertiesNames()
  {
    List<string> results = [];
    foreach (var item in typeof(TLocal).GetRuntimeProperties())
    {

      if(item.PropertyType == typeof(string))
        results.Add(item.Name);
    }
    return results;
  }

  public override async Task<Boolean> UpdateEntity(T entity, String entityName = "")
  {
    throw new NotImplementedException();
  }
}
