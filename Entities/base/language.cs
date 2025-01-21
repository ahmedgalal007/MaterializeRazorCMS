using AspnetCoreStarter.Entities.Articles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Entities;

public class Language : BaseEntity<string>
{
  public Language() { }
  public Language(string twoLettersCode, string isoCode, string name, string localName, bool isEnabled = true, bool isRTL = false, bool isDefault = false) {
    Id = isoCode;
    Name = name;
    LocalName = localName;
    TwoLettersCode = twoLettersCode;
    // IsoCode = isoCode;
    IsEnabled = isEnabled;
    IsRTL = isRTL;
    IsDefault = isDefault;
  }

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Length(5, 6, ErrorMessage = "language code must be 5 to 6 Letters")]
  public new string Id { get; set; }
  [Length(3, 50, ErrorMessage = "The language name must be between 3 - 50 Letters")]
  public string Name { get; set; }
  public string LocalName { get; set; }

  [Length(2,2,ErrorMessage = "Two Letters language code must be 2 Letters")]
  public string TwoLettersCode { get; set; }

  public string IsoCode => Id;
  public bool IsEnabled { get; set; } = true;
  public bool IsRTL { get; set; } = false;
  public bool IsDefault { get; set; } = false;
  public virtual List<Article> Articles { get; set; } 
}
