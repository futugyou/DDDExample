namespace Example.Application.UnitTest;

public class EventSourcingHandlerUnitTest
{
    [Fact]
    public async Task HandlerWithNullEventShouldThrowArgumentNullExceptionTest()
    {
        // Arrage
        var repository = new Mock<IEventStoreRepository<AggregateRoot>>();
        repository.Setup(a => a.AppendAsync(It.IsAny<EventStore>()))
            .Verifiable();
        long version = 0;

        // Act
        var handler = new EventSourcingHandler<AggregateRoot>(repository.Object);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, version));
    }

    [Fact]
    public async Task HandlerWithEventShouldCallAppendAsyncTest()
    {
        // Arrage
        var repository = new Mock<IEventStoreRepository<AggregateRoot>>();
        repository.Setup(a => a.AppendAsync(It.IsAny<EventStore>()))
            .Verifiable();
        long version = 0;
        var @event = new CustomerRegisterEvent(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CustomerLevel>());

        // Act
        var handler = new EventSourcingHandler<AggregateRoot>(repository.Object);
        await handler.Handle(@event, version);

        // Assert
        repository.Verify(a => a.AppendAsync(It.IsAny<EventStore>()), Times.Once, "append async must called once");
    }
}
