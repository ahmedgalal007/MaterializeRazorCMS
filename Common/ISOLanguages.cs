
using AspnetCoreStarter.Entities;
using Microsoft.AspNetCore.Http;

namespace AspnetCoreStarter.Common
{

  // public record Language(string Name, string LocalName, string TwoLettersCode, string ISOCode, bool IsEnabled = true, bool IsRTL = false, bool IsDefault = false);


  public interface IISOLanguages
  {
    public static readonly List<Language> languages;
  }
  public class ISOLanguages : IISOLanguages
  {
    public static readonly List<Language> languages = new(){
      new Language("ar", "ar-EG", "Arabic Egypt", "العربية مصر", true,true,true),
      new Language("ar", "ar-SA", "Arabic Saudi Arabia", "العربية السعودية", true,true),
      new Language("ar", "ar-UAE", "Arabic UAE", "العربية الأمارات", true,true),
      new Language("en", "en-US", "English", "English"),
      new Language("fr", "fr-FR", "French", "French", false),
      new Language("du", "du-GE", "German", "German", false),
    };
  }


}
