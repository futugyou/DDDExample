using Christ.Domain.Core.Commands;
using Example.Domain.Core.Bus;
using Example.Domain.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastruct.Data.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task RaiseEvent<T>(T @event) where T : Event
        {
            //除去领域通知，全部进事件溯源
            if (!@event.MessageType.Equals("DomainNotification"))
                await _eventStore?.Save(@event);
            await _mediator.Publish(@event);
        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }
    }
}
