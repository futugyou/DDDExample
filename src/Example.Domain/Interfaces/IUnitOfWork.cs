namespace Example.Domain;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}
