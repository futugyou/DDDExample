namespace Example.Domain.Core;
public interface IEventSourcing
{
    long Version { get; }

    void ValidateVersion(long expectedVersion);

    IEnumerable<IDomainEvent> GetUncommittedEvents();

    void ClearUncommittedEvents();
}
