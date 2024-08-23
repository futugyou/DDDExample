namespace Example.Domain.Core;
public abstract class AggregateRoot : Entity, IEventSourcing
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private readonly List<IDomainEvent> _domainEvents = [];

    public long Version { get; private set; } = -1;

    public void AddDomainEvent(IDomainEvent @event, long originalVersion = -1)
    {
        ValidateVersion(originalVersion);
        @event.BuildVersion(Version + 1);
        ApplyEvent(@event, @event.AggregateVersion);
        _domainEvents.Add(@event);
    }

    public void ValidateVersion(long version)
    {
        if (Version != version)
        {
            throw new ConcurrencyException("Invalid version specified");
        }
    }

    public IEnumerable<IDomainEvent> GetUncommittedEvents()
    {
        return _domainEvents.AsEnumerable();
    }

    public void ClearUncommittedEvents()
    {
        _domainEvents.Clear();
    }

    public void ApplyEvent(IDomainEvent @event, long version)
    {
        if (!_domainEvents.Any(x => Equals(x.EventId, @event.EventId)))
        {
            ((dynamic)this).Apply((dynamic)@event);
            Version = version;
        }
    }

    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event, @event.AggregateVersion);
        }
    }
}
