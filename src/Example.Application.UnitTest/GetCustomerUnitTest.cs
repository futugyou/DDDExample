namespace Example.Application.UnitTest;
public class GetCustomerUnitTest
{
    private static Customer customer = new Customer(Guid.NewGuid(), "thisisname", "q@c.com", DateTime.Now);

    public GetCustomerUnitTest()
    {

    }

    [Fact]
    public async Task GetAllCustomerSuccessTest()
    {
        // Arrange
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        var customers = new List<Customer> { customer }.AsQueryable();
        _customerRepository.Setup(p => p.GetAll()).ReturnsAsync(customers);

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        var users = await service.GetAll();

        // Assert
        Assert.True(users != null);
        Assert.True(users.Count() == 1);
    }

    [Fact]
    public async Task GetAllCustomerWithNodataSuccessTest()
    {
        // Arrange
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        var customers = new List<Customer> { customer }.AsQueryable();

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        var users = await service.GetAll();

        // Assert
        Assert.True(users != null);
        Assert.True(users.Count() == 0);
    }

    [Fact]
    public async Task GetCustomerByidSuccessTest()
    {
        // Arrange
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        _customerRepository.Setup(c => c.GetById(customer.Id)).ReturnsAsync(customer);

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        var users = await service.GetById(customer.Id);

        // Assert
        Assert.Equal(users.Id, customer.Id);
    }

    [Fact]
    public async Task GetCustomerByidWithNodataSuccessTest()
    {
        // Arrange
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        _customerRepository.Setup(c => c.GetById(Guid.Empty));

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        var user = await service.GetById(customer.Id);

        // Assert
        Assert.True(user == null);
    }
}
