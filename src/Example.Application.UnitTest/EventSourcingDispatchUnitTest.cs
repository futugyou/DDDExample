namespace Example.Application.UnitTest;

public class EventSourcingDispatchUnitTest
{
    [Fact]
    public async Task DispatchWithNoEventsShouldNotCallHandlerTest()
    {
        // Arrage
        var handler = new Mock<IEventSourcingHandler<IDomainEvent>>();
        handler.Setup(e => e.Handle(It.IsAny<IDomainEvent>(), It.IsAny<long>())).Verifiable();

        var aggregate = new Mock<IEventSourcing>();

        // Act
        var dispatch = new EventSourcingDispatch(handler.Object);
        await dispatch.Dispatch(aggregate.Object);

        // Assert
        handler.Verify(e => e.Handle(It.IsAny<IDomainEvent>(), It.IsAny<long>()), Times.Never, "handler must not be called");
        Assert.Equal(0, aggregate.Object.Version);
    }

    [Fact]
    public async Task DispatchWithEventsShouldCallHandlerOnceTest()
    {
        // Arrage
        var handler = new Mock<IEventSourcingHandler<IDomainEvent>>();
        handler.Setup(e => e.Handle(It.IsAny<IDomainEvent>(), It.IsAny<long>()))
            .Returns(Task.FromResult(true))
            .Verifiable();

        var aggregate = new Mock<IEventSourcing>();
        aggregate.Setup(a => a.GetUncommittedEvents())
            .Returns(new List<IDomainEvent>
            {
                new CustomerRegisterEvent(It.IsAny<Guid>(), It.IsAny<string>(),  It.IsAny<string>(),  It.IsAny<DateTime>())
            });

        // Act
        var dispatch = new EventSourcingDispatch(handler.Object);
        await dispatch.Dispatch(aggregate.Object);

        // Assert
        handler.Verify(e => e.Handle(It.IsAny<IDomainEvent>(), It.IsAny<long>()), Times.Once, "handler must not be called once");
        Assert.Equal(0, aggregate.Object.Version);
        Assert.Single(aggregate.Object.GetUncommittedEvents());
    }
}
