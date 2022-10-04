namespace Example.Infrastruct.Data;
public class EventStoreRepository  : IEventStoreRepository 
{ 
    private readonly DbSet<EventStore> _dbSet; 

    public EventStoreRepository(CustomerContext databaseContext)
    {
        if (databaseContext == null)
        {
            throw new ArgumentNullException(nameof(databaseContext));
        } 
        _dbSet = databaseContext.Set<EventStore>();
    }

    public async Task AppendAsync(EventStore @event)
    {
        await _dbSet.AddAsync(@event);
    }

    public Task<T> GetByIdAsync<T>(Guid id) where T : AggregateRoot
    {
        throw new NotImplementedException();
    }
}