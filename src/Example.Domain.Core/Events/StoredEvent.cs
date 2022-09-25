namespace Example.Domain.Core;

public class StoredEvent : Event
{
    protected StoredEvent()
    {
    }

    public StoredEvent(Event @event, string data, string user)
    {
        Id = Guid.NewGuid();
        AggregateId = @event.AggregateId;
        MessageType = @event.MessageType;
        Data = data;
        User = user;
    }
    public Guid Id { get; private set; }
    public string Data { get; private set; }
    public string User { get; private set; }
}
