namespace Example.Domain.Core;
internal interface IEventSourcing
{
    long Version { get; }

    void ValidateVersion(long expectedVersion);

    IEnumerable<IDomainEvent> GetUncommittedEvents();

    void ClearUncommittedEvents();
}
