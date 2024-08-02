
namespace Example.Application.UnitTest;

public class RegisterCustomerUnitTest
{
    [Fact]
    public async Task RegisterCustomerSuccessTest()
    {
        // Arrange
        var _mapper = new Mock<IMapper>();
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        var userViewModel = new CustomerViewModel
        {
            BirthDate = DateTime.Now,
            City = "shanghai",
            County = "xuhuiqu",
            Email = "shanghai@sh.com",
            Name = "wangxiaoming",
            Province = "shanghai",
            Street = "tianlinlu 100 hao",
        };

        // Act
        using var service = new CustomerAppService(_customerRepository.Object, _mapper.Object, _mediatorHandler.Object);
        await service.Register(userViewModel);

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task RegisterCustomerFailedTest()
    {
        // Arrange
        var _mapper = new Mock<IMapper>();
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, _mapper.Object, _mediatorHandler.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.Register(null!));
    }
}