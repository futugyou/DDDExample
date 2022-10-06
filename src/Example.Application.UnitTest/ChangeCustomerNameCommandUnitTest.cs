using MediatR;

namespace Example.Application.UnitTest;
public class ChangeCustomerNameCommandUnitTest
{
    [Fact]
    public async Task ChangeCustometNameWithNullCommandShouldThrowArgumentNullExceptionTest()
    {
        // Arrage
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        ChangeCustomerNameCommand request = null;

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, default));
    }

    [Fact]
    public async Task ChangeCustometNameWithNullNameShouldReturnUnitTest()
    {
        // Arrage
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        ChangeCustomerNameCommand request = new ChangeCustomerNameCommand(Guid.NewGuid(), null);
        var result = await handler.Handle(request, default);

        // Assert
        Assert.Equal(result, Unit.Value);
    }

    [Fact]
    public async Task ChangeCustometNameDonotFindCustomerShouldReturnUnitTest()
    {
        // Arrage
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();

        ChangeCustomerNameCommand request = new ChangeCustomerNameCommand(Guid.NewGuid(), "this is new name");
        repository.Setup(m => m.GetById(request.Id)).Returns(Task.FromResult((Customer)null));

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        var result = await handler.Handle(request, default);

        // Assert
        Assert.Equal(result, Unit.Value);
    }

    [Fact]
    public async Task ChangeCustometNameSuccessShouldReturnUnitTest()
    {
        // Arrage
        var unitOfWork = new Mock<IUnitOfWork>();
        var mediatorHandler = new Mock<IMediatorHandler>();
        var repository = new Mock<ICustomerRepository>();
        var dispatch = new Mock<IEventSourcingDispatch>();
        var customer = new Customer(Guid.NewGuid(), "this is name", "email@e.com", DateTime.UtcNow);
        ChangeCustomerNameCommand request = new ChangeCustomerNameCommand(customer.Id, "this is new name");
        repository.Setup(m => m.GetById(request.Id)).Returns(Task.FromResult(customer));
        repository.Setup(m => m.Update(customer)).Returns(Task.CompletedTask).Verifiable();

        // Act
        var handler = new CustomerCommandHandler(unitOfWork.Object,
                                                 mediatorHandler.Object,
                                                 repository.Object,
                                                 dispatch.Object);
        var result = await handler.Handle(request, default);

        // Assert
        repository.Verify(m => m.Update(
            It.Is<Customer>(
                n => n.Id.Equals(customer.Id)
                && n.Name.Equals(request.Name)
                && n.GetType().Equals(customer.GetType())
                )),
                Times.Once);
        Assert.Equal(result, Unit.Value);
    }
}
