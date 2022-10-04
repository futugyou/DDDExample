namespace Example.Infrastruct.Data;
public class DomainEventRebuilder : IDomainEventRebuilder
{
    public IEnumerable<DomainEvent> RebuildDomainEvents(IEnumerable<EventStore> events)
    {
        throw new NotImplementedException();
    }
}
