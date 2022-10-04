using Example.Domain;
using Example.Domain.Core;
using Example.Infrastruct.Data;

namespace Example.Infrastruct.UnitTest;
public class EventStoreRepositoryUnitTest
{
    [Fact]
    public async Task AppendAsyncShouldOkTest()
    {
        // arrange
        var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
        optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
        var context = new CustomerContext(optionsBuilder.Options);
        var @event = new EventStore(
            Guid.NewGuid(),
            1,
            "name",
            "typename",
            DateTime.UtcNow,
            "{}"
            );
        IEventStoreRepository<Customer> storeRepository = new EventStoreRepository<Customer>(context, It.IsAny<IAggregateInvoker<Customer>>(), It.IsAny<IDomainEventRebuilder>());

        // act
        await storeRepository.AppendAsync(@event);
        await context.SaveChangesAsync();
        var result = await context.EventStores.FirstOrDefaultAsync();

        // assert
        Assert.NotNull(result);
        Assert.Equal(result.Id, @event.Id);
    }

    [Fact]
    public async Task GetByIdAsyncErrorTest()
    {
        // arrange 
        var context = new Mock<CustomerContext>();
        IEventStoreRepository<Customer> storeRepository = new EventStoreRepository<Customer>(context.Object, It.IsAny<IAggregateInvoker<Customer>>(), It.IsAny<IDomainEventRebuilder>());
        var id = Guid.Empty;

        // act

        // assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => storeRepository.GetByIdAsync(id));
    }

    [Fact]
    public async Task GetByIdAsyncSuccessTest()
    {
        // arrange 
        var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
        optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
        var context = new CustomerContext(optionsBuilder.Options);
        var invoker = new AggregateInvoker<Customer>();
        var storeRepository = new EventStoreRepository<Customer>(context, invoker, It.IsAny<IDomainEventRebuilder>());
        var id = Guid.NewGuid();

        // act
        var aggregate = await storeRepository.GetByIdAsync(id);

        // assert
        Assert.NotNull(aggregate);
    }

}
