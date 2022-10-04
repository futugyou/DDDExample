using Example.Domain;
using Example.Domain.Core;

namespace Example.Infrastruct.UnitTest;
public class AggregateInvokerUnitTest
{
    [Fact]
    public void AggregateInvokerCreateInstanceTest()
    {
        // arrange
        IAggregateInvoker<Customer> invoker = new AggregateInvoker<Customer>();

        // act
        var customer = invoker.CreateInstanceOfAggregateRoot();

        // assert
        Assert.NotNull(customer);
    }
}
