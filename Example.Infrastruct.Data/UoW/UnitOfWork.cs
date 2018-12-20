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
        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
