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

  public List<KeyValuePair<string, string>> Query(string q, string entity)
  {
    List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
    switch (entity)
    {
      case "Category":
        return _context.Categories.Include(x => x.Locales).Where(x => x.QueryLocales("Name", q)).Select(s => new KeyValuePair<string, string>(s.Locales.First().Name, s.Id.ToString())).ToList();
      default:
        break;
    }
    // _context.Set<TEntity>().Where(x => EF.Property<string>(x, textField) == q).ToList();

    return result;
  }
}
