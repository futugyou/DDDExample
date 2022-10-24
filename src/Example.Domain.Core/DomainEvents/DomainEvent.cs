namespace Example.Domain.Core;

public abstract class DomainEvent : IDomainEvent
{
    public Guid AggregateId { get; protected set; }
    public Guid EventId => Guid.NewGuid();
    public long AggregateVersion { get; private set; }
    public DateTime Timestamp { get; } = DateTime.Now;

    public void BuildVersion(long aggregateVersion)
    {
        AggregateVersion = aggregateVersion;
    }
}