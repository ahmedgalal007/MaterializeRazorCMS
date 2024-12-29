using AspnetCoreStarter.Common;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Locals;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspnetCoreStarter.Pages.Apps.Categories
{
  public class CategoryModel : LocalizedCRUDPageModel<Category, CategoryLocals, Guid>
  {
    public CategoryModel(ApplicationDbContext context) : base(context)
    {
      // TableItems = TableItems.AsQueryable().Include(x => x.Locales).ToList();
    }

    public override async Task OnGetAsync(int page = 1, int take = 10)
    {
      CurrentPage = page;
      Total = await _dbSet.CountAsync();
      TableItems = await _dbSet
        .Include(e => e.Locales)
        .Include(e => e.Parent)
        .ThenInclude(x => x.Locales)
        .Skip(Start)
        .Take(take)
        .ToListAsync();
    }
    public override async Task<Boolean> BeforeUpdate(Category entity)
    {
      if (!await base.BeforeUpdate(entity)) return false;
      foreach (var item in entity.Locales)
      {

      }
      return true;
    }

    public override async Task<Boolean> UpdateEntity(Category entity, String entityName = "NewEntry")
    {

      //await _dbSet.Entry(entity).Collection(e => e.Locales).LoadAsync();
      var entityToUpdate = await LoadLocales(entity);
      // if (string.IsNullOrWhiteSpace(entityName)) entityName = "NewEntry";
      if (entityToUpdate.Locales.Count > 0)
      {
        await TryUpdateModelAsync(entityToUpdate, entityName, u => u.CategoryURI, u => u.Slug, u => u.ParentId, u => u.Color);
        foreach (var item in entityToUpdate.Locales.Select((e, i) => new { value = e, index = i }))
        {
          await TryUpdateModelAsync(item.value, entityName + $".Locales[{item.index}]", u => u.Id, u => u.LanguageID, u => u.Name);
        }

      }

      return await Task.FromResult(true);
    }
  }
}
