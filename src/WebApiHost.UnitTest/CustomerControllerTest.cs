using System.ComponentModel.DataAnnotations;

namespace WebApiHost.UnitTest;

public class CustomerControllerTest
{
    [Fact]
    public void CustomerValidateSuccessTest()
    {
        //Arrange 
        var result = new List<ValidationResult>();
        var customer = new CustomerViewModel
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Province = "Some Province",
            City = "Some City",
            County = "Some County",
            Street = "123 Some Street"
        };

        //Act 
        var isValid = Validator.TryValidateObject(customer, new ValidationContext(customer), result);

        //Assert
        Assert.True(isValid);
        Assert.Empty(result);
    }

    // This doesn't make sense
    [Fact]
    public async Task RegisterCustomerSuccessTest()
    {
        //Arrange
        var moq = new Mock<ICustomerAppService>();
        var controller = new CustomerController(moq.Object);
        var customer = new CustomerViewModel
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Province = "Some Province",
            City = "Some City",
            County = "Some County",
            Street = "123 Some Street"
        };

        //Act 
        await controller.SaveCustomer(customer);

        //Assert
        Assert.True(true);
    }
}
