using AspnetCoreStarter.Common;

namespace AspnetCoreStarter.Pages.Shared.DisplayTemplates;

public class AutoCompleteModel
{
  public AutoCompleteModel(AutoCompeleteHelper helper)
  {
    Items = helper.Query("", "Category");
  }
  public List<KeyValuePair<string, string>> Items { get; set; }
}
