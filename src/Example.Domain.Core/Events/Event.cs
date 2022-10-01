namespace Example.Domain.Core;

public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }
    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}

// TODO: Refactoring
// Temporarily hold both events
public abstract class DomainEvent : INotification
{
    public DateTime Timestamp { get; private set; }
    protected DomainEvent()
    {
        Timestamp = DateTime.Now;
    }
}