using Example.Domain.Core.Exceptions;
using System.Reflection;

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
        long _version = 0;
        var createEvent = new CustomerRegisterEvent(Guid.NewGuid(), "name", "e@e.com", DateTime.UtcNow);
        var customr = CreateNewAggregate<Customer>();

        // Act
        customr?.ApplyEvent(createEvent, _version);

        // Assert
        Assert.Equal(createEvent.AggregateId, customr?.Id);

        Assert.IsAssignableFrom<DomainEvent>(createEvent);
    }

    [Fact]
    public void ApplyEvent()
    {
        // Arrage
        long _version = -1;
        IEventSourcing sourcing = new StubEventSourcing();
        // Act
        sourcing.ValidateVersion(_version);
        // Assert
        Assert.True(sourcing.Version == _version);
    }

    private T? CreateNewAggregate<T>() where T : AggregateRoot
    {
        var t = typeof(T)
            .GetConstructor(BindingFlags.Instance |
                                     BindingFlags.NonPublic |
                                     BindingFlags.Public,
                            null,
                            new Type[0],
                            new ParameterModifier[0])
            ?.Invoke(new object[0]);
        return t as T;
    }
}
