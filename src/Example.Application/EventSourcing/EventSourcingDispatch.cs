namespace Example.Application;

public class EventSourcingDispatch : IEventSourcingDispatch
{
    private readonly IEventSourcingHandler<IDomainEvent> _handler;

    public EventSourcingDispatch(IEventSourcingHandler<IDomainEvent> handler)
    {
        _handler = handler;
    }

    public async Task Dispatch(IEventSourcing aggregate)
    {
        foreach (var evt in aggregate.GetUncommittedEvents())
        {
            await _handler.Handle(evt, aggregate.Version);
        }
    }
}
