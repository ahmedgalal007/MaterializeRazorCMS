using AspnetCoreStarter.Entities.Articles;
using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStarter.Entities;

public class Language : BaseEntity<int>
{
  public Language() { }
  public Language(string name, string localName, string twoLettersCode, string isoCode, bool isEnabled = true, bool isRTL = false, bool isDefault = false) {
    Name = name;
    LocalName = localName;
    TwoLettersCode = twoLettersCode;
    IsoCode = isoCode;
    IsEnabled = isEnabled;
    IsRTL = isRTL;
    IsDefault = isDefault;
  }

  [Length(3, 50, ErrorMessage = "The language name must be between 3 - 50 Letters")]
  public string Name { get; set; }
  public string LocalName { get; set; }

  [Length(2,2,ErrorMessage = "Two Letters language code must be 2 Letters")]
  public string TwoLettersCode { get; set; }

  [Length(5, 6, ErrorMessage = "language code must be 5 to 6 Letters")]
  public string IsoCode { get; set; }
  public bool IsEnabled { get; set; } = true;
  public bool IsRTL { get; set; } = false;
  public bool IsDefault { get; set; } = false;
  public virtual List<Article> Articles { get; set; } 
}
