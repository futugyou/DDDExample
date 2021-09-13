using System;
using Example.Domain.Models;
using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastruct.Data.Repository
{
    /// <summary>
    /// Customer仓储，操作对象还是领域对象
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext context) : base(context)
        {
        }
        //对特例接口进行实现
        public async Task<Customer> GetByEmail(string email) => await _dbSet.FirstOrDefaultAsync(p => p.Email == email);
    }
}
