namespace Example.Application;

public interface IEventSourcingHandler<T> where T : IDomainEvent
{
    Task Handle(T @event, long aggregateVersion);
}