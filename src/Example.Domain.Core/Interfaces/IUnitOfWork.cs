namespace Example.Domain.Core;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}
