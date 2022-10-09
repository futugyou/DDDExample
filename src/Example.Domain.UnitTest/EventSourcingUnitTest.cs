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
    public void ApplyEventTest()
    {
        // Arrage
        long _version = 0;
        var createEvent = new CustomerRegisterEvent(Guid.NewGuid(), "name", "e@e.com", DateTime.UtcNow);
        var customr = CreateNewAggregate<Customer>();

        // Act
        customr?.ApplyEvent(createEvent, _version);

        // Assert
        Assert.Equal(createEvent.AggregateId, customr?.Id);

        Assert.IsAssignableFrom<IDomainEvent>(createEvent);
        Assert.Equal(createEvent.AggregateVersion, customr?.Version);
    }

    [Fact]
    public void CallGetUncommittedEventsShouldGetDomainEventTest()
    {
        // Arrage
        var customer = CreateNewAggregate<Customer>();

        // Act
        var result = customer?.GetUncommittedEvents();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.IsAssignableFrom<IEnumerable<IDomainEvent>>(result);
    }

    [Fact]
    public void CallClearUncommittedEventsShouldClearDomainEventTest()
    {
        // Arrage
        var id = Guid.NewGuid();
        var name = "12345";
        var email = "";
        var customer = new Customer(id, name, email, It.IsAny<DateTime>());

        // Act
        var events = customer.GetUncommittedEvents();

        // Assert
        Assert.Single(events);
        customer.ClearUncommittedEvents();
        events = customer.GetUncommittedEvents();
        Assert.Empty(events);
    }

    [Fact]
    public void CallLoadFromHistoryMethodShouldRenderCurrentStateTest()
    {
        // Arrage
        var newname = "thisisnewname";

        var customr = CreateNewAggregate<Customer>();

        var registerEvent = new CustomerRegisterEvent(customr.Id, customr.Name, customr.Email, customr.BirthDate);
        registerEvent.BuildVersion(0);

        var changeNameEvent = new CustomerChangeNameEvent(customr.Id, newname);
        changeNameEvent.BuildVersion(1);

        var events = new List<IDomainEvent>
        {
            registerEvent,
            changeNameEvent,
        };

        // Act
        customr.LoadFromHistory(events);

        // Assert
        Assert.Equal(newname, customr.Name);
        Assert.Equal(1, customr.Version);
    }

    [Fact]
    public void AddDomainEventWithInvalidVesrionTest()
    {
        //Arrange
        long expectedVersion = 0;
        var sut = CreateNewAggregate<StubEventSourcing>();
        //Act
        //Assert
        Assert.Throws<ConcurrencyException>(()
            => sut.ExposeAddDomainEvent(It.IsAny<IDomainEvent>(), expectedVersion));
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
