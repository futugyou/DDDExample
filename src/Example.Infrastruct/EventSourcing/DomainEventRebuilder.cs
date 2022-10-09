using System.Text.Json;

namespace Example.Infrastruct;
public class DomainEventRebuilder : IDomainEventRebuilder
{
    public IEnumerable<IDomainEvent> RebuildDomainEvents(IEnumerable<EventStore> events)
    {
        var op = new JsonSerializerOptions();
        return events.Select(e => System.Text.Json.JsonSerializer.Deserialize(e.PayLoad, Type.GetType(e.TypeName), op) as IDomainEvent);
    }
}
