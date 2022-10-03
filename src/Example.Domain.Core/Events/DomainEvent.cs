namespace Example.Domain.Core;

// TODO: Refactoring
// Temporarily hold both events
public abstract class DomainEvent : IDomainEvent
{
    public Guid AggregateId { get; protected set; }
    public Guid EventId => Guid.NewGuid();
    public long AggregateVersion { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.Now;

    public void BuildVersion(long aggregateVersion)
    {
        AggregateVersion = aggregateVersion;
    }
}