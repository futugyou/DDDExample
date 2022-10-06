namespace Example.Domain;

public class CustomerChangeNameEvent : DomainEvent
{
    public string Name { get; private set; }

    public CustomerChangeNameEvent(Guid id, string newName)
    {
        AggregateId = id;
        Name = newName;
    }
}
