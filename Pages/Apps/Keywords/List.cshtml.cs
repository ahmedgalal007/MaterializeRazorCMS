using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Keywords;
using System.Data.Entity;
using System.Linq.Expressions;

namespace AspnetCoreStarter.Pages.Apps.Keywords;

public class KeywordModel : CRUDPageModel<Keyword,Guid> 
{
  public KeywordModel(ApplicationDbContext context) : base(context)
  {
  }

  public override async Task<Boolean> UpdateEntity(Keyword entity, String entityName = "")
  {
    // return base.OnUpdateEntity(entity, entityName);
    // if (string.IsNullOrWhiteSpace(entityName)) entityName = NewEntry.GetType().Name.ToLower();
    if (string.IsNullOrWhiteSpace(entityName)) entityName = "NewEntry";
    await TryUpdateModelAsync(entity, entityName, u => u.KeywordURI, u => u.Slug, u => u.Schema);

    return await Task.FromResult(true);
  }

}
