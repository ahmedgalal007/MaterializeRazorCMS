namespace AspnetCoreStarter.Pages.Shared.EditorTemplates;


public class Select2Model
{
  public Select2Model(string inputName, List<KeyValuePair<string, string>> options,string cssClass="",bool isMultiple=false, bool isDisabled=false, string[]? selectedValues = default, string id = "", Dictionary<string,Object> attributes=default)
  {
    Name = inputName;
    Options = options;
    IsMultiple = isMultiple;
    IsDisabled = isDisabled;
    SelectedValues = selectedValues??[];
    Id = id;
    Attributes = attributes ?? new();
  }
  public bool IsMultiple { get; set; } = false;
  public bool IsDisabled { get; set; } = false;
  public string Name { get; set; }
  public string Id { get; set; } = string.Empty;
  public string[] SelectedValues { get; set; }  = [];
  public List<KeyValuePair<string, string>> Options { get; }
  public Dictionary<string, Object> Attributes { get; }

  public bool IsItemSelected(string value) {  return SelectedValues.Contains(value); }
}


