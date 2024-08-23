namespace Example.Application;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand>, IRequestHandler<ChangeCustomerNameCommand>, IDisposable
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventSourcingDispatch _eventSourcingDispatch;

    public CustomerCommandHandler(IUnitOfWork unitOfWork,
                                  IMediatorHandler mediatorHandler,
                                  ICustomerRepository customerRepository,
                                  IEventSourcingDispatch eventSourcingDispatch)
        : base(unitOfWork, mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
        _customerRepository = customerRepository;
        _eventSourcingDispatch = eventSourcingDispatch;
    }

    public void Dispose()
    {
        _customerRepository.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task Handle(RegisterCustomerCommand request, CancellationToken _)
    {
        if (!request.IsValid())
        {
            await NotifyValidationErrors(request);
            return;
        }

        var customer = await _customerRepository.GetByEmail(request.Email);
        if (customer is not null)
        {
            //domain notification
            await _mediatorHandler.RaiseEvent(new DomainNotification(customer.Id.ToString(), "email address already exists"));
            return;
        }

        customer = new Customer(request.Id, request.Name, request.Email, request.BirthDate);

        await _customerRepository.Add(customer);
        await _eventSourcingDispatch.Dispatch(customer);
        await CommitAsync();

        return;
    }

    public async Task Handle(ChangeCustomerNameCommand request, CancellationToken _)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!request.IsValid())
        {
            await NotifyValidationErrors(request);
            return;
        }

        var customer = await _customerRepository.GetById(request.Id);
        if (customer is null)
        {
            //domain notification
            await _mediatorHandler.RaiseEvent(new DomainNotification(request.Id.ToString(), "no customer found"));
            return;
        }

        customer.ChangeName(request.Name, customer.Version);
        await _customerRepository.Update(customer);
        await _eventSourcingDispatch.Dispatch(customer);
        await CommitAsync();

        return;
    }
}
