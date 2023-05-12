namespace Example.Domain.UnitTest;

public class CustomerUnitTest
{
    [Fact]
    public void CustomerWithoutIDTest()
    {
        // Arrange
        var id = Guid.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new Customer(id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1234")]
    [InlineData(UnitTool.LongString)]
    public void CustomerWithInvalidNameTest(string name)
    {
        // Arrange
        var id = Guid.NewGuid();
        // Act
        // Assert
        Assert.Throws<CustomerNameCheckException>(() => new Customer(id, name, It.IsAny<string>(), It.IsAny<DateTime>()));
    }

    [Fact]
    public void CustomerSuccessTest()
    {
        // Arrange
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
        Assert.True(customer.Id == customerCreateEvent.AggregateId);
    }

    [Fact]
    public void ChangeNameWhenExpectedVersionIsNotEqualsToAggregateVersionShouldThrowConcurrencyExceptionTest()
    {
        //Arrange
        var expectedVersion = 1;
        var id = Guid.NewGuid();
        var name = "12345";
        var email = "";
        var customer = new Customer(id, name, email, It.IsAny<DateTime>());

        var newName = "5678900";

        //Act
        //Assert
        Assert.Throws<ConcurrencyException>(() => customer.ChangeName(newName, expectedVersion));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1234")]
    [InlineData(UnitTool.LongString)]
    public void ChangeNameWhenNullOrEmptyShouldThrowAggregateExceptionTest(string newName)
    {
        //Arrange
        var id = Guid.NewGuid();
        var name = "12345";
        var email = "";
        var customer = new Customer(id, name, email, It.IsAny<DateTime>());

        //Act
        //Assert
        Assert.Throws<AggregateException>(() => customer.ChangeName(newName, It.IsAny<long>()));
    }

    [Fact]
    public void ChangeNameWithValiidArgumentsShouldApplyCustomerChangeNameEventTest()
    {
        //Arrange
        var expectedVersion = 0;
        var id = Guid.NewGuid();
        var name = "12345";
        var email = "";
        var customer = new Customer(id, name, email, It.IsAny<DateTime>());

        var newName = "5678900";

        //Act
        customer.ChangeName(newName, expectedVersion);

        //Assert
        Assert.Equal(newName, customer.Name);
    }
}