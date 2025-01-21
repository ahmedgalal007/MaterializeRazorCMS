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

  public List<KeyValuePair<string, string>> Query(string q, string entity, string id="",string parentId = "", List<Guid>? excludeIds = null)
  {
    List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
    switch (entity)
    {
      case "Category":
        return _context.Categories.Include(x => x.Locales)
          .Where(x => x.Locales.Any(x => string.IsNullOrWhiteSpace(q)?true: x.Name.Contains(q)))
          .Where(x => string.IsNullOrWhiteSpace(id) ? true: x.Id == new Guid(id))
          .Where(x => string.IsNullOrWhiteSpace(parentId) ? true: x.ParentId == new Guid(parentId))
          .Where(x => (excludeIds == null)? true: !excludeIds.Contains(x.Id))
          .Select(s => new KeyValuePair<string, string>(s.Locales.First().Name, s.Id.ToString()))
          .ToList();
      case "Attribute":
        return _context.Attributes
          .Where(x => string.IsNullOrWhiteSpace(q) ? true : x.Name.Contains(q))
          .Where(x => string.IsNullOrWhiteSpace(id) ? true : x.Id == new Guid(id))
          .Where(x => (excludeIds == null) ? true : !excludeIds.Contains(x.Id))
          .Select(s => new KeyValuePair<string, string>(s.Name, s.Id.ToString()))
          .ToList();
      case "PostType":
        return _context.PostTypes
          .Where(x => string.IsNullOrWhiteSpace(q) ? true : x.Name.Contains(q))
          .Where(x => string.IsNullOrWhiteSpace(id) ? true : x.Id == new Guid(id))
          .Where(x => (excludeIds == null) ? true : !excludeIds.Contains(x.Id))
          .Select(s => new KeyValuePair<string, string>(s.Name, s.Id.ToString()))
          .ToList();
      default:
        break;
    }
    // _context.Set<TEntity>().Where(x => EF.Property<string>(x, textField) == q).ToList();

    return result;
  }
}
