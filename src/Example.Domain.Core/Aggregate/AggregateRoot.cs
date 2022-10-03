using Example.Domain.Core.Exceptions;

namespace Example.Domain.Core;
public abstract class AggregateRoot : Entity, IEventSourcing
{
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
    private List<DomainEvent> _domainEvents = new();

    public long Version => _version;
    private long _version = -1;

    public void AddDomainEvent(DomainEvent @event, long originalVersion = -1)
    {
        ValidateVersion(originalVersion);
        @event.BuildVersion(_version + 1);
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

    public void ApplyEvent(DomainEvent @event, long version)
    {
        if (!_domainEvents.Any(x => Equals(x.EventId, @event.EventId)))
        {
            ((dynamic)this).Apply((dynamic)@event);
            _version = version;
        }
    }
}
