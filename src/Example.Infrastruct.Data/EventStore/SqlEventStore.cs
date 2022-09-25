namespace Example.Infrastruct.Data;

public class SqlEventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    public SqlEventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }
    public async Task Save<T>(T @event) where T : Event
    {
        var serializedData = JsonConvert.SerializeObject(@event);
        var storedEvent = new StoredEvent(@event, serializedData, "");
        await _eventStoreRepository.Store(storedEvent);
    }
}
