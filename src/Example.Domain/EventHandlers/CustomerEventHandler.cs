﻿namespace Example.Domain;

public class CustomerEventHandler : INotificationHandler<CustomerRegisterEvent>
{

    public Task Handle(CustomerRegisterEvent notification, CancellationToken cancellationToken)
    {
        //DOTO:
        return Task.CompletedTask;
    }
}
