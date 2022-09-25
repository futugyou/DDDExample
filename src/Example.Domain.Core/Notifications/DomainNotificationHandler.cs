using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            //Handler Notification
            return Task.CompletedTask;
        }
    }
}
