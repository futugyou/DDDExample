namespace Example.Domain.Core;

public abstract class Message : IRequest
{
    public Guid AggregateId { get; set; }
    public string MessageType { get; set; }
    protected Message()
    {
        MessageType = GetType().Name;
    }
}
