namespace Example.Domain.Core;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    long AggregateVersion { get; }
    DateTime Timestamp { get; }
    void BuildVersion(long aggregateVersion);
}
