namespace Example.Infrastruct;

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
