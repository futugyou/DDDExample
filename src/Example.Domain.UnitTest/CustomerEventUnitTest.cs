namespace Example.Domain.UnitTest;

public class CustomerEventUnitTest
{
    [Fact]
    public void ApplyEventTest()
    {
        // Arrange
        long _version = 0;
        var createEvent = new CustomerRegisterEvent(Guid.NewGuid(), "name", "e@e.com", DateTime.UtcNow, CustomerLevel.Comman);
        var customr = UnitTool.CreateNewAggregate<Customer>();

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
        // Arrange
        var customer = UnitTool.CreateNewAggregate<Customer>();

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
        // Arrange
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
        // Arrange
        var newname = "thisisnewname";

        var customr = UnitTool.CreateNewAggregate<Customer>()!;

        var registerEvent = new CustomerRegisterEvent(customr.Id, customr.Name, customr.Email, customr.BirthDate, customr.CustomerLevel);
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
}
