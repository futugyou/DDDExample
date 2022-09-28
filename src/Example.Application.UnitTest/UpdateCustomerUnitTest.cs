namespace Example.Application.UnitTest;
public class UpdateCustomerUnitTest
{
    [Fact]
    public async Task UpdateCustomerSuccessTest()
    {
        // Arrage
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        var customerViewModel = new CustomerViewModel
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
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        await service.Update(customerViewModel);

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task UpdateCustomerFailedTest()
    {
        // Arrage
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();

        // Act
        ICustomerAppService service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(null));
    }
}
