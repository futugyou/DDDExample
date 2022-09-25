namespace Example.Domain;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand>, IDisposable
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly ICustomerRepository _customerRepository;
    public CustomerCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler, ICustomerRepository customerRepository)
        : base(unitOfWork, mediatorHandler)
    {
        _unitOfWork = unitOfWork;
        _mediatorHandler = mediatorHandler;
        _customerRepository = customerRepository;
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
        var customer = new Customer(request.Id, request.Name, request.Email, request.BirthDate);
        if (await _customerRepository.GetByEmail(customer.Email) != null)
        {
            //domain notification
            await _mediatorHandler.RaiseEvent(new DomainNotification(customer.Id.ToString(), "email address already exists"));
            return Unit.Value;
        }
        await _customerRepository.Add(customer);
        if (await CommitAsync())
        {
            //domain event
            await _mediatorHandler.RaiseEvent(new CustomerRegisterEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
        }
        return Unit.Value;
    }
}
