using AspnetCoreStarter.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace AspnetCoreStarter.Entities;


public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
  protected BaseEntity() => Id = Guid.NewGuid();
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
  public TId Id { get; protected set; } = default!;

  [NotMapped]
  public List<DomainEvent> DomainEvents { get; } = new();
}

//public class BaseEntity : BaseEntity<Guid>
//{
//}
//  public class BaseEntity<T>
//{
//  [Key]
//  public T Id { get; set; }
//  public virtual List<IEvent> Events { get; set; }
//  public TId Id { get; protected set; } = default!;

//  [NotMapped]
//  public List<DomainEvent> DomainEvents { get; } = new();
//  public void ClearEvents()
//  {
//    Events.Clear();
//  }
//}
