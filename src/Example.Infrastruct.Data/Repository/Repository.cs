using System;
using System.Linq;
using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastruct.Data.Repository
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CustomerContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(CustomerContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task Add(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Remove(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public Task Update(TEntity obj)
        {
            _dbSet.Update(obj);
            return Task.CompletedTask;
        }
    }
}
