
using Microsoft.AspNetCore.Http;

namespace AspnetCoreStarter.Common
{

  public record Language(string Name, string LocalName, string TwoLettersCode, string ISOCode, bool IsEnabled = true, bool IsRTL = false, bool IsDefault = false);


  public interface IISOLanguages
  {
    public static readonly List<Language> languages;
  }
  public class ISOLanguages : IISOLanguages
  {
    public static readonly List<Language> languages = new(){
      new Language("English", "English", "en", "en-US"),
      new Language("Arabic", "عربي", "ar", "ar-EG", true,true,true),
      new Language("German", "German", "du", "du-GE"),
      new Language("French", "French", "fr", "fr-FR"),
    };
  }


}
