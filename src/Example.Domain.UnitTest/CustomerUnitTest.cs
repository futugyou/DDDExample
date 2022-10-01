namespace Example.Domain.UnitTest;

public class CustomerUnitTest
{
    [Fact]
    public void CustomerWithoutIDTest()
    {
        // Arrage
        var id = Guid.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Customer(id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
    }

    [Fact]
    public void CustomerWithoutNameTest()
    {
        // Arrage
        var id = Guid.NewGuid();
        var name = "";
        // Act
        // Assert
        Assert.Throws<AggregateException>(() => new Customer(id, name, It.IsAny<string>(), It.IsAny<DateTime>()));
    }

    [Fact]
    public void CustomerSuccessTest()
    {
        // Arrage
        var id = Guid.NewGuid();
        var name = "12345";
        var email = "";
        // Act
        var customer = new Customer(id, name, email, It.IsAny<DateTime>());
        var domainEvent = customer.DomainEvents.FirstOrDefault();
        // Assert
        Assert.True(customer.Id == id);
        Assert.IsAssignableFrom<CustomerRegisterEvent>(domainEvent);
        Assert.NotNull(domainEvent);
        var customerCreateEvent = (CustomerRegisterEvent)domainEvent;
        Assert.NotNull(customerCreateEvent);
        Assert.True(customer.Id == customerCreateEvent.Id);
    }
}