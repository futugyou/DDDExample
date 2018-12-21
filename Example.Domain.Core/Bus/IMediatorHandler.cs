using System;
using System.Threading.Tasks;
using Christ.Domain.Core.Commands;
using Example.Domain.Core.Events;

namespace Example.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
