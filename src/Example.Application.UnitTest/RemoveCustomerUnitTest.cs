namespace Example.Application.UnitTest;
public class RemoveCustomerUnitTest
{
    [Fact]
    public async Task RemoveCustomerSuccessTest()
    {
        // Arrage
        var _mediatorHandler = new Mock<IMediatorHandler>();
        var _customerRepository = new Mock<ICustomerRepository>();
        // Act
        using var service = new CustomerAppService(_customerRepository.Object, AutoMapperHelper.mapper, _mediatorHandler.Object);
        await service.Remove(Guid.NewGuid());

        // Assert
        Assert.True(true);
    }
}
