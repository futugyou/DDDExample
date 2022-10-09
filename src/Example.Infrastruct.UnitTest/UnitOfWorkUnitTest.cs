namespace Example.Infrastruct.UnitTest;

public class UnitOfWorkUnitTest
{
    [Fact]
    public async Task Commit()
    {
        // arrange
        var context = new Mock<CustomerContext>();
        context.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1).Verifiable();

        // act
        var unitOfWork = new UnitOfWork(context.Object);
        await unitOfWork.CommitAsync();

        // assert
        context.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public void Dispose()
    {
        // arrange
        var context = new Mock<CustomerContext>();
        context.Setup(c => c.Dispose()).Verifiable();

        // act
        IUnitOfWork unitOfWork = new UnitOfWork(context.Object);
        unitOfWork.Dispose();

        // assert
        context.Verify(c => c.Dispose(), Times.Once);
    }
}