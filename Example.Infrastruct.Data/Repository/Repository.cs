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
        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }
    }
}
