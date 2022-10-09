namespace Example.Infrastruct;
public interface IDomainEventRebuilder
{
    IEnumerable<IDomainEvent> RebuildDomainEvents(IEnumerable<EventStore> events);
}
