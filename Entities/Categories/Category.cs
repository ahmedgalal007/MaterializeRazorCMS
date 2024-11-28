using AspnetCoreStarter.Entities.Locals;

namespace AspnetCoreStarter.Entities.Categories;

public class Category : MultiLangEntity<CategoryLocals, Guid>
{
  public Uri? CategoryURI { get; set; }
  public string Slug { get; set; } = string.Empty;

  public override CategoryLocals AddLocal(CategoryLocals local)
  {
    Locales.Add(local);
    return local;
  }

  public override CategoryLocals RemoveLocal(CategoryLocals local)
  {
    Locales.Remove(local);
    return local;
  }

  public override CategoryLocals UpdateLocal(CategoryLocals local)
  {
    var current = Locales.Where(e => e.Id == local.Id).FirstOrDefault();
    if (current == null)
      return local;
    // return new KeyNotFoundException($"The localized version with id ({local.Id.ToString()}) not exsists");
    current.Name = local.Name;
    return current;
  }
}
