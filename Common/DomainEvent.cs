namespace AspnetCoreStarter.Common;

public abstract class DomainEvent : IEvent
{
  public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}