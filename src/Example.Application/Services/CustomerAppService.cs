namespace Example.Application;

/// <summary>
/// CustomerAppService 服务接口实现类,继承 服务接口
/// 通过 DTO 实现视图模型和领域模型的关系处理
/// 作为调度者，协调领域层和基础层，
/// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
/// </summary>
public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    public CustomerAppService(
        ICustomerRepository customerRepository,
        IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<IEnumerable<CustomerViewModel>> GetAll()
    {
        var domain = await _customerRepository.GetAll();
        if (domain == null)
        {
            return Enumerable.Empty<CustomerViewModel>();
        }

        return domain.ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
    }

    public async Task<CustomerViewModel> GetById(Guid id)
    {
        var domain = await _customerRepository.GetById(id);
        if (domain == null)
        {
            return null;
        }

        return _mapper.Map<CustomerViewModel>(domain);
    }

    public async Task Register(CustomerViewModel customerViewModel)
    {
        if (customerViewModel == null)
        {
            throw new ArgumentNullException(nameof(customerViewModel));
        }

        var registerCommand = _mapper.Map<RegisterCustomerCommand>(customerViewModel);
        //command bus
        await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task Update(CustomerViewModel customerViewModel)
    {
        if (customerViewModel == null)
        {
            throw new ArgumentNullException(nameof(customerViewModel));
        }

        await _customerRepository.Update(_mapper.Map<Customer>(customerViewModel));
    }

    public async Task Remove(Guid id)
    {
        await _customerRepository.Remove(id);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
