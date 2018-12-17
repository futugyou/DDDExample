using System;
using Example.Application.Interfaces;
using Example.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHost.Controllers
{
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
        public void SaveCustomer(CustomerViewModel customer)
        {
            _customerAppService.Register(customer);
        }

        [HttpGet]
        public CustomerViewModel GetCustomer(Guid id)
        {
            return _customerAppService.GetById(id);
        }
    }
}
