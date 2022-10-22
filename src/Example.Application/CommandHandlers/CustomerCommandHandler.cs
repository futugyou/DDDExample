namespace Example.Application;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand>, IDisposable
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
    }

    public async Task<Unit> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
        {
            await NotifyValidationErrors(request);
            return Unit.Value;
        }

        var customer = await _customerRepository.GetByEmail(request.Email);
        if (customer != null)
        {
            //domain notification
            await _mediatorHandler.RaiseEvent(new DomainNotification(customer.Id.ToString(), "email address already exists"));
            return Unit.Value;
        }

        customer = new Customer(request.Id, request.Name, request.Email, request.BirthDate);

        await _customerRepository.Add(customer);
        await _eventSourcingDispatch.Dispatch(customer);
        await CommitAsync();

        return Unit.Value;
    }

    public async Task<Unit> Handle(ChangeCustomerNameCommand request, CancellationToken _)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (!request.IsValid())
        {
            await NotifyValidationErrors(request);
            return Unit.Value;
        }

        var customer = await _customerRepository.GetById(request.Id);
        if (customer == null)
        {
            //domain notification
            await _mediatorHandler.RaiseEvent(new DomainNotification(request.Id.ToString(), "no customer found"));
            return Unit.Value;
        }

        customer.ChangeName(request.Name, customer.Version);
        await _customerRepository.Update(customer);
        await _eventSourcingDispatch.Dispatch(customer);
        await CommitAsync();

        return Unit.Value;
    }
}
