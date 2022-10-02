namespace Example.Infrastruct.UnitTest;
public class DatabaseContextUnitTest
{
    [Fact]
    public void DatabaseContextTest()
    {
        //Arrange
        var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
        optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
        var context = new CustomerContext(optionsBuilder.Options);

        //Act
        context.Database.EnsureCreated();
        context.Dispose();

        //Assert
        Assert.NotNull(context.Customers);
    }
}
