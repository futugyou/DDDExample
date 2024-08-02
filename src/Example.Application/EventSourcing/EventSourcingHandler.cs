namespace Example.Application;

public class EventSourcingHandler<T> : IEventSourcingHandler<IDomainEvent> where T : AggregateRoot
{
    private readonly IEventStoreRepository<T> _eventStoreRepository;

    public EventSourcingHandler(IEventStoreRepository<T> eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(IDomainEvent @event, long aggregateVersion)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var serializedBody = JsonSerializer.Serialize(@event);

        var eventStore = new EventStore(@event.AggregateId, aggregateVersion,
            $"{aggregateVersion}@{@event.AggregateId}",
            @event.GetType().AssemblyQualifiedName ?? "",
            @event.Timestamp,
            serializedBody);
        await _eventStoreRepository.AppendAsync(eventStore);
    }
}
