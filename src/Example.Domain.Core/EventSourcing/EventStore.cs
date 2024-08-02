namespace Example.Domain.Core;

public record EventStore
{
    public int Id { get; private set; }
    public Guid AggregateId { get; private set; }
    public long Version { get; private set; }
    public string Name { get; private set; }
    public string TypeName { get; private set; }
    public DateTime Created { get; private set; }
    public string PayLoad { get; private set; }

    public EventStore(Guid aggregateId,
                     long version,
                     string name,
                     string typeName,
                     DateTime created,
                     string payLoad)
    {
        AggregateId = aggregateId;
        Version = version;
        Name = name;
        TypeName = typeName;
        Created = created;
        PayLoad = payLoad;
    }
}