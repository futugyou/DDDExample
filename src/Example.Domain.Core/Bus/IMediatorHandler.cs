namespace Example.Domain.Core;

public interface IMediatorHandler
{
    //command
    Task SendCommand<T>(T command) where T : Command;

    //event
    Task RaiseEvent<T>(T @event) where T : Event;
}
