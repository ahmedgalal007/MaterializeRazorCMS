using AspnetCoreStarter.Entities.Locals;

namespace AspnetCoreStarter.Entities.Categories;

public class Keyword : MultiLangEntity<KeywordLocals, Guid>
{
  public Uri? KeywordURI { get; set; }
  public string Slug { get; set; } = string.Empty;

  /// <summary>
  /// Keyword type (Multible) (Person || Product || Company || Place || Event )
  /// </summary>
  public string Schema { get; set; } = string.Empty;
  public override KeywordLocals AddLocal(KeywordLocals local)
  {
    Locales.Add(local);
    return local;
  }

  public override KeywordLocals RemoveLocal(KeywordLocals local)
  {
    Locales.Remove(local);
    return local;
  }

  public override KeywordLocals UpdateLocal(KeywordLocals local)
  {
    var current = Locales.Where(e => e.Id == local.Id).FirstOrDefault();
    if (current == null)
      return local;
    // return new KeyNotFoundException($"The localized version with id ({local.Id.ToString()}) not exsists");
    current.Title = local.Title;
    current.Description = local.Description;

    return current;
  }
}
