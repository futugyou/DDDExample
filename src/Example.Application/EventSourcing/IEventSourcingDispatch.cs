namespace Example.Application;

public interface IEventSourcingDispatch
{
    Task Dispatch(IEventSourcing aggregate);
}
