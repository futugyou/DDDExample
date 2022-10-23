namespace Example.Domain.UnitTest;

public class EventSourcingUnitTest
{
    [Fact]
    public void ValidateVersionThrowConcurrencyExceptionTest()
    {
        // Arrage
        long _version = 0;
        IEventSourcing sourcing = new StubEventSourcing();

        // Act
        // Assert
        Assert.Throws<ConcurrencyException>(() => sourcing.ValidateVersion(_version));
    }

    [Fact]
    public void ValidateVersionOkTest()
    {
        // Arrage
        long _version = -1;
        IEventSourcing sourcing = new StubEventSourcing();

        // Act
        sourcing.ValidateVersion(_version);

        // Assert
        Assert.True(sourcing.Version == _version);
    }

    [Fact]
    public void AddDomainEventWithInvalidVesrionTest()
    {
        //Arrange
        long expectedVersion = 0;
        var sut = UnitTool.CreateNewAggregate<StubEventSourcing>();
        //Act
        //Assert
        Assert.Throws<ConcurrencyException>(()
            => sut.ExposeAddDomainEvent(It.IsAny<IDomainEvent>(), expectedVersion));
    }
}
