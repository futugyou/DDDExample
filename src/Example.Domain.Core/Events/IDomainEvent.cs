namespace Example.Domain.Core;

public interface IDomainEvent : INotification
{
    Guid AggregateId { get; }
    Guid EventId { get; }
    long AggregateVersion { get; }
    DateTime Timestamp { get; }
    void BuildVersion(long aggregateVersion);
}
