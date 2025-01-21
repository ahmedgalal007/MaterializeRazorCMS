using System.Text.RegularExpressions;

namespace AspnetCoreStarter.Common;

public static class CommonHelpers
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="phrase"></param>
  /// <param name="max">Default 45, = 0 To disable slug trim</param>
  /// <returns> slugified string </returns>
  public static string GenerateSlug(this string phrase, int max = 45)
  {
    string str = phrase.RemoveAccent().ToLower();
    // invalid chars           
    str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
    // convert multiple spaces into one space   
    str = Regex.Replace(str, @"\s+", " ").Trim();
    // cut and trim
    if (max > 0) 
    str = str.Substring(0, str.Length <= max ? str.Length : max).Trim();
    str = Regex.Replace(str, @"\s", "-"); // hyphens   
    return str;
  }

  public static string RemoveAccent(this string txt)
  {
    byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
    return System.Text.Encoding.ASCII.GetString(bytes);
  }
}
