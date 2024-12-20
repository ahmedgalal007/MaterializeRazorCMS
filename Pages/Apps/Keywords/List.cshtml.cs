using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Keywords;
using AspnetCoreStarter.Entities.Locals;
using System.Data.Entity;
using System.Linq.Expressions;

namespace AspnetCoreStarter.Pages.Apps.Keywords;

public class KeywordModel : LocalizedCRUDPageModel<Keyword, KeywordLocals, Guid>
{
  public KeywordModel(ApplicationDbContext context) : base(context)
  {
    if (NewEntry.Locales.Count == 0) NewEntry.Locales.Add(new(DefaultLanguage));
  }

  public override async Task<Boolean> BeforeUpdate(Keyword entity)
  {
    if (!await base.BeforeUpdate(entity)) return false;
    foreach (var item in entity.Locales)
    {

    }
    return true;
  }

  public override async Task<Boolean> UpdateEntity(Keyword entity, String entityName = "")
  {
    // return base.OnUpdateEntity(entity, entityName);
    // if (string.IsNullOrWhiteSpace(entityName)) entityName = NewEntry.GetType().Name.ToLower();
    await _dbSet.Entry(entity).Collection(e => e.Locales).LoadAsync();
    if (string.IsNullOrWhiteSpace(entityName)) entityName = "NewEntry";
    if (entity.Locales.Count > 0)
      await TryUpdateModelAsync(entity, entityName, u => u.KeywordURI, u => u.Slug, u => u.Schema, u => u.Locales);
    
    return await Task.FromResult(true);
  }

}
