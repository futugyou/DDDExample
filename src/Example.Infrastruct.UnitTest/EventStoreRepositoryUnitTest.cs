using Example.Domain.Core;

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
        IEventStoreRepository storeRepository = new EventStoreRepository(context);

        // act
        await storeRepository.AppendAsync(@event);
        await context.SaveChangesAsync();
        var result = await context.EventStores.FirstOrDefaultAsync();

        // assert
        Assert.NotNull(result);
        Assert.Equal(result.Id,@event.Id);
    }
}
