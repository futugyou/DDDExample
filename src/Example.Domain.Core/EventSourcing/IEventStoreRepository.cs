namespace Example.Domain.Core;

public interface IEventStoreRepository
{
    Task<T> GetByIdAsync<T>(Guid id) where T : AggregateRoot;
    Task AppendAsync(EventStore @event);
}
