using AspnetCoreStarter.Entities.Locals;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace AspnetCoreStarter.Entities.Posts;

public class PostAttribute : MultiLangEntity<PostAttributeLocals,Guid>
{
  public Attribute Type { get; set; } = new Attribute();
  public DateTime ValueDate { get; set; } = new DateTime();
  public double ValueNumber { get; set; }
  public bool ValueBoolean { get; set; }
  [NotMapped]
  public Func<string,string> ValueText => (string lang) => Locales.Where(e => e.Language.Code == lang).Select(e => e.ValueText).First();
  public override PostAttributeLocals AddLocal(PostAttributeLocals local)
  {
    Locales.Add(local);
    return local;
  }

  public override PostAttributeLocals RemoveLocal(PostAttributeLocals local)
  {
    Locales.Remove(local);
    return local;
  }

  public override PostAttributeLocals UpdateLocal(PostAttributeLocals local)
  {
    var current = Locales.Where(e => e.Id == local.Id).FirstOrDefault();
    if (current == null)
      return local;
    // return new KeyNotFoundException($"The localized version with id ({local.Id.ToString()}) not exsists");
    current.ValueText = local.ValueText;
    return current;
  }
}
