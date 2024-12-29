using AspnetCoreStarter.Data;
using Microsoft.EntityFrameworkCore;
namespace AspnetCoreStarter.Common;

public class AutoCompeleteHelper
{
  private readonly ApplicationDbContext _context;

  public AutoCompeleteHelper(ApplicationDbContext context)
  {
    _context = context;
  }

  public List<KeyValuePair<string, string>> Query(string q, string entity, string id="",string parentId = "")
  {
    List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
    switch (entity)
    {
      case "Category":
        return _context.Categories.Include(x => x.Locales)
          .Where(x => x.Locales.Any(x => string.IsNullOrWhiteSpace(q)?true: x.Name.Contains(q)))
          .Where(x => string.IsNullOrWhiteSpace(id) ? true: x.Id == new Guid(id))
          .Where(x => string.IsNullOrWhiteSpace(parentId) ? true: x.ParentId == new Guid(parentId))
          .Select(s => new KeyValuePair<string, string>(s.Locales.First().Name, s.Id.ToString()))
          .ToList();
      default:
        break;
    }
    // _context.Set<TEntity>().Where(x => EF.Property<string>(x, textField) == q).ToList();

    return result;
  }
}
