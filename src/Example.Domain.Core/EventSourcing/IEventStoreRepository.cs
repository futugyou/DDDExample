namespace Example.Domain.Core;

public interface IEventStoreRepository<T> where T : AggregateRoot
{
    Task<T> GetByIdAsync(Guid id);
    Task AppendAsync(EventStore @event);
}
