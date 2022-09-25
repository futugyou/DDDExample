using Christ.Domain.Core.Commands;
using Example.Domain.Core.Events;

namespace Example.Domain.Core.Bus;

public interface IMediatorHandler
{
    //command
    Task SendCommand<T>(T command) where T : Command;

    //event
    Task RaiseEvent<T>(T @event) where T : Event;
}
