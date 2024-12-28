namespace AspnetCoreStarter.Pages.Shared.EditorTemplates;


public class Select2Model
{
  public Select2Model(string inputName, List<KeyValuePair<string, string>> options,bool isMultiple=false, bool isDisabled=false, string[]? selectedValues = default, string id = "")
  {
    Name = inputName;
    Options = options;
    IsMultiple = isMultiple;
    IsDisabled = isDisabled;
    SelectedValues = selectedValues??[];
    Id = id;
  }
  public bool IsMultiple { get; set; } = false;
  public bool IsDisabled { get; set; } = false;
  public string Name { get; set; }
  public string Id { get; set; } = string.Empty;
  public string[] SelectedValues { get; set; }  = [];
  public List<KeyValuePair<string, string>> Options { get; }

  public bool IsItemSelected(string value) {  return SelectedValues.Contains(value); }
}


