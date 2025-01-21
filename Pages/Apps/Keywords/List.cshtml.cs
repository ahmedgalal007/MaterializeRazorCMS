using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Keywords;
using AspnetCoreStarter.Entities.Locals;

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

  public override async Task<Boolean> UpdateEntity(Keyword entity, String entityName = "NewEntry")
  {
    // return base.OnUpdateEntity(entity, entityName);
    // if (string.IsNullOrWhiteSpace(entityName)) entityName = NewEntry.GetType().Name.ToLower();
    // await _dbSet.Entry(entity).Collection(e => e.Locales).LoadAsync();

    if (string.IsNullOrWhiteSpace(entityName)) entityName = "NewEntry";
    //if (entity.Locales.Count > 0)
    //{
      // var Locales = entity.Locales.Select(x => x).ToList();
      await TryUpdateModelAsync(entity, entityName, u => u.KeywordURI, u => u.Slug, u => u.Schema, u => u.Locales);
      //foreach (var item in Locales)
      //{
      //  entity.Locales.FirstOrDefault(x => x.LanguageID == item.LanguageID).Id = item.Id;
      //}
      //foreach (var item in entity.Locales.Select((e, i) => new { value = e, index = i }))
      //{
      //  await TryUpdateModelAsync(item.value, entityName + $".Locales[{item.index}]", u => u.Id, u => u.LanguageID, u => u.Title, u => u.Description);
      //}

    //}
    return await Task.FromResult(true);
  }

}
