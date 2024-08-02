namespace Example.Application.UnitTest;
public class ChangeCustomerNameCommandUnitTest
{
    [Fact]
    public async Task ChangeCustomerNameWithNullCommandShouldThrowArgumentNullExceptionTest()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        ChangeCustomerNameCommand? request = null;

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request!, default));
    }

    [Fact]
    public async Task ChangeCustomerNameWithNullNameShouldReturnUnitTest()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        var request = new ChangeCustomerNameCommand(Guid.NewGuid(), null!);
        await handler.Handle(request, default);

        // Assert
    }

    [Fact]
    public async Task ChangeCustomerNameNoFindCustomerShouldReturnUnitTest()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        var request = new ChangeCustomerNameCommand(Guid.NewGuid(), "this is new name");
        Customer? customer = null;
        repository.Setup(m => m.GetById(request.Id)).Returns(Task.FromResult(customer));

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        await handler.Handle(request, default);

        // Assert
    }

    [Fact]
    public async Task ChangeCustomerNameSuccessShouldReturnUnitTest()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();
        var customer = new Customer(Guid.NewGuid(), "this is name", "email@e.com", DateTime.UtcNow);
        var request = new ChangeCustomerNameCommand(customer.Id, "this is new name");
        repository.Setup(m => m.GetById(request.Id)).Returns(Task.FromResult<Customer?>(customer));
        repository.Setup(m => m.Update(customer)).Returns(Task.CompletedTask).Verifiable();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        await handler.Handle(request, default);

        // Assert
        repository.Verify(m => m.Update(
            It.Is<Customer>(
                n => n.Id.Equals(customer.Id)
                && n.Name.Equals(request.Name)
                && n.GetType().Equals(customer.GetType())
                )),
                Times.Once);
    }
}
