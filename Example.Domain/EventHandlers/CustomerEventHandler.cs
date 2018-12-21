using Example.Domain.Core.Bus;
using Example.Domain.Events.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.EventHandlers
{
    public class CustomerEventHandler : INotificationHandler<CustomerRegisterEvent>
    {

        public Task Handle(CustomerRegisterEvent notification, CancellationToken cancellationToken)
        {
            //DOTO:
            return Task.CompletedTask;
        }
    }
}
