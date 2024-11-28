using AspnetCoreStarter.Data;

namespace AspnetCoreStarter.Pages.Apps.Keywords;

public class ListModel:BasePageModel
{
  private ApplicationDbContext _context { get; }

  public ListModel(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task OnGetAsync()
  {

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
