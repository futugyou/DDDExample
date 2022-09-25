namespace Example.Infrastruct.Data;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreSQLContext _eventStoreSQLContext;
    public EventStoreRepository(EventStoreSQLContext eventStoreSQLContext)
    {
        _eventStoreSQLContext = eventStoreSQLContext;
    }
    public async Task<IList<StoredEvent>> All(Guid aggregateId)
    {
        return await _eventStoreSQLContext.StoredEvents.Where(p => p.AggregateId == aggregateId).ToListAsync();
    }

    public void Dispose()
    {
        _eventStoreSQLContext.Dispose();
    }

    public async Task Store(StoredEvent storedEvent)
    {
        _eventStoreSQLContext.Add(storedEvent);
        await _eventStoreSQLContext.SaveChangesAsync();
    }
}
