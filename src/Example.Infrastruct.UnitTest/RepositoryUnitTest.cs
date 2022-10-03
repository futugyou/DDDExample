using Example.Domain;
using Example.Domain.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastruct.UnitTest;
public class RepositoryUnitTest
{
    [Fact]
    public async Task CreateCustomerTest()
    {
        // arrange
        var dbSet = GetQueryableMockDbSet(new List<EntityAsAggregateRoot>
        {
                new EntityAsAggregateRoot()
        });

        var context = new Mock<CustomerContext>();
        context.Setup(c => c.Set<EntityAsAggregateRoot>()).Returns(dbSet.Object);

        // act
        IRepository<EntityAsAggregateRoot> repository = new Repository<EntityAsAggregateRoot>(context.Object);
        await repository.Add(new EntityAsAggregateRoot());

        // assert
        dbSet.Verify(m => m.AddAsync(It.IsAny<EntityAsAggregateRoot>(), It.IsAny<CancellationToken>()),
                Times.Once, "Commit must be called only once");
    }

    private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();

        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

#pragma warning disable CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
        dbSet
            .Setup(_ => _.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
            .Callback((T model, CancellationToken token) => { sourceList.Add(model); })
            .Returns((T model, CancellationToken token) => ValueTask.FromResult(default(EntityEntry<T>)))
            .Verifiable();
#pragma warning restore CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。

        return dbSet;
    }
}
public class EntityAsAggregateRoot : AggregateRoot
{
}