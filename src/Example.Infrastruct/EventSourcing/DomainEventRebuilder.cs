using System.Text.Json;

namespace Example.Infrastruct;
public class DomainEventRebuilder : IDomainEventRebuilder
{
    public IEnumerable<IDomainEvent> RebuildDomainEvents(IEnumerable<EventStore> events)
    {
        var op = new JsonSerializerOptions();
        var domainEvents = new List<IDomainEvent>();

        foreach (var e in events)
        {
            var type = Type.GetType(e.TypeName) ?? throw new InvalidOperationException($"Type '{e.TypeName}' could not be found.");

            if (JsonSerializer.Deserialize(e.PayLoad, type, op) is IDomainEvent domainEvent)
            {
                domainEvents.Add(domainEvent);
            }
            else
            {
                throw new InvalidOperationException($"Deserialization of event '{e.TypeName}' failed.");
            }
        }

        return domainEvents;
    }

}
