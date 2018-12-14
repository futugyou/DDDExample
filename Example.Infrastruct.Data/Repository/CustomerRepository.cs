using System;
using Example.Domain.Models;
using Example.Domain.Interfaces;

namespace Example.Infrastruct.Data.Repository
{
    /// <summary>
    /// Customer仓储，操作对象还是领域对象
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        //对特例接口进行实现
        public Customer GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
