using Example.Domain.Core;
using Moq;

namespace Example.Application.UnitTest;
public class GetCustomerUnitTest
{
    private static Customer customer;

    public GetCustomerUnitTest()
    {
        customer = new Customer(Guid.NewGuid(), "name", "q@c.com", DateTime.Now);
    }

    [Fact]
    public async Task GetAllCustomerSuccessTest()
    {
        // Arrage
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
        // Arrage
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
        // Arrage
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
        // Arrage
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
