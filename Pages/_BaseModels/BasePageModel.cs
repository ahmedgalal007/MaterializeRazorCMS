using AspnetCoreStarter.Common;
using AspnetCoreStarter.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages;

public class BasePageModel : PageModel
{
  public BasePageModel()
  {
    Language = ISOLanguages.languages.FirstOrDefault(e => e.IsDefault);
  }
  public Language? Language { get; set; }
}
