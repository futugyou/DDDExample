namespace Example.Application;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        //Handler Notification
        return Task.CompletedTask;
    }
}
