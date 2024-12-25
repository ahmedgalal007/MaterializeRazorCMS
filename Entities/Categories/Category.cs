using AspnetCoreStarter.Entities.Locals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreStarter.Entities.Categories;

public class Category : MultiLangEntity<CategoryLocals, Guid>
{
  public Uri? CategoryURI { get; set; }
  public string Slug { get; set; } = string.Empty;

  public Guid? ParentId { get; set; }
  public virtual Category? Parent { get; set; }

  [ForeignKey(nameof(ParentId))]
  public virtual List<Category> Childrens { get; set; } = new List<Category>();

  [Length(4,8)]
  public string Color { get; set; } = "#ffffff";
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
