namespace Example.Domain;

public record CustomerChangeNameEvent : DomainEvent
{
    public string Name { get; }

    public CustomerChangeNameEvent(Guid id, string newName)
    {
        AggregateId = id;
        Name = newName;
    }
}
