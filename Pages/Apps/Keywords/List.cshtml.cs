using AspnetCoreStarter.Data;
using AspnetCoreStarter.Entities.Categories;
using AspnetCoreStarter.Entities.Keywords;
using System.Data.Entity;

namespace AspnetCoreStarter.Pages.Apps.Keywords;

public class ListModel:BasePageModel
{
  private ApplicationDbContext _context { get; }

  public ListModel(ApplicationDbContext context)
  {
    _context = context;
  }

  public int Page { get; set; } = 1;
  public int Take { get; set; } = 10 ;
  public int Start => Take * (Page - 1);
  public int End => Start + Take;
  public int Total { get; set; } = 0;
  public IEnumerable<Keyword> KeywordsList { get; set; }

  public async Task OnGetAsync(int page=1, int take=10)
  {
    KeywordsList = await _context.Keywords.Skip((Page - 1)* take).Take(take).ToListAsync();
  }

  public async Task<IActionResult> OnPostAsync()
  {

    return RedirectToPage();
  }

  public async Task<IActionResult> OnPostEditKeywordAsync()
  {

    return RedirectToPage();
  }

  public async Task<IActionResult> OnPostDeleteKeywordAsync()
  {

    return RedirectToPage();
  }
}
