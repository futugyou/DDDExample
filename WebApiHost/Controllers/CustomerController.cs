using System;
using Microsoft.AspNetCore.Mvc;
using WebApiHost.Models;

namespace WebApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// 保存顾客方法:add & update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="birthDate"></param>
        [HttpPost]
        public void SaveCustomer(string id, string name, string email, string birthDate)
        {
            Customer customer = CustomerDao.GetCustomer(id);
            if (customer == null)
            {
                customer = new Customer();
                customer.Id = id;
            }

            if (name != null)
            {
                customer.Name = name;
            }
            if (email != null)
            {
                customer.Email = email;
            }

            //...还有其他属性

            CustomerDao.SaveCustomer(customer);
        }

        [HttpGet]
        public Customer GetCustomer(string id)
        {
            return CustomerDao.GetCustomer(id);
        }
    }
}
