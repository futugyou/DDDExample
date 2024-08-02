namespace Example.Infrastruct;
public class EventStoreRepository<T> : IEventStoreRepository<T> where T : AggregateRoot
{
    private readonly DbSet<EventStore> _dbSet;
    private readonly IAggregateInvoker<T> _invoker;
    private readonly IDomainEventRebuilder _domainEventRebuilder;

    public EventStoreRepository(CustomerContext databaseContext,
                                IAggregateInvoker<T> invoker,
                                IDomainEventRebuilder domainEventRebuilder)
    {
        if (databaseContext == null)
        {
            throw new ArgumentNullException(nameof(databaseContext));
        }

        _dbSet = databaseContext.Set<EventStore>();
        _invoker = invoker;
        _domainEventRebuilder = domainEventRebuilder;
    }

    public async Task AppendAsync(EventStore @event)
    {
        await _dbSet.AddAsync(@event);
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var aggregate = _invoker.CreateInstanceOfAggregateRoot();
        if (aggregate is null)
        {
            throw new ArgumentNullException(nameof(id), "CreateInstanceOfAggregateRoot return null");
        }

        var eventItemss = _dbSet.AsNoTracking().Where(p => p.AggregateId == id);
        if (!eventItemss.Any())
        {
            return Task.FromResult(aggregate);
        }

        var events = _domainEventRebuilder.RebuildDomainEvents(eventItemss);
        aggregate.LoadFromHistory(events);

        return Task.FromResult(aggregate);
    }
}