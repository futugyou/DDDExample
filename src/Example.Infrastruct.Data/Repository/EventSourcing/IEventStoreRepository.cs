namespace Example.Infrastruct.Data;

public interface IEventStoreRepository : IDisposable
{
    Task Store(StoredEvent storedEvent);
    Task<IList<StoredEvent>> All(Guid aggregateId);
}
