using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Context;

namespace Example.Infrastruct.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerContext _dbContext;
        public UnitOfWork(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CommitAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
