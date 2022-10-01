namespace WebApiHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    ICustomerAppService _customerAppService;
    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;

    }
    /// <summary>
    /// 保存顾客方法:add & update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="birthDate"></param>
    [HttpPost]
    public async Task SaveCustomer(CustomerViewModel customer)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException(nameof(customer));
        }

        await _customerAppService.Register(customer);
    }

    /// <summary>
    /// 7246784B-782D-4B9F-965E-E2FD3547FC90
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<CustomerViewModel> GetCustomer(Guid id)
    {
        return await _customerAppService.GetById(id);
    }
}
