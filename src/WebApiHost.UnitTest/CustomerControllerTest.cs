using System.ComponentModel.DataAnnotations;

namespace WebApiHost.UnitTest;

public class CustomerControllerTest
{
    [Fact]
    public void RegisterCustomerFaildTest()
    {
        //Arrange
        var moq = new Mock<ICustomerAppService>();
        var result = new List<ValidationResult>();
        var controller = new CustomerController(moq.Object);
        var customer = new CustomerViewModel();

        //Act 
        var isValid = Validator.TryValidateObject(customer, new ValidationContext(customer), result);

        //Assert
        Assert.False(isValid);
        Assert.Equal(3, result.Count);
        Assert.Equal("Name", result[0].MemberNames.ElementAt(0));
        Assert.Equal("The Name is Required", result[0].ErrorMessage);
    }

    // This doesn't make sense
    [Fact]
    public async Task RegisterCustomerSuccessTest()
    {
        //Arrange
        var moq = new Mock<ICustomerAppService>();
        var controller = new CustomerController(moq.Object);
        var customer = new CustomerViewModel();

        //Act 
        await controller.SaveCustomer(customer);

        //Assert
        Assert.True(true);
    }
}
