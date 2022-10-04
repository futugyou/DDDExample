namespace Example.Domain.Core;

public class EventStore
{
    public int Id { get; private set; }
    public long Version { get; private set; }
    public Guid AggregateId { get; private set; }
    public string Name { get; private set; }
    public string TypeName { get; private set; }
    public DateTime Created { get; private set; }
    public string PayLoad { get; private set; }
    public bool IsSync { get; private set; }

    private EventStore()
    {
    }

    public EventStore(Guid aggregateId,
                      long aggregateVersion,
                      string name,
                      string typeName,
                      DateTime created,
                      string serializedBody)
    {
        Version = aggregateVersion;
        AggregateId = aggregateId;
        Name = name;
        TypeName = typeName;
        Created = created;
        PayLoad = serializedBody;
    }

    public void MarkAsSynced()
    {
        IsSync = true;
    }
}