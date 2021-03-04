using System;
using System.Collections.Generic;
using Example.Application.Interfaces;
using Example.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApiHost.Extensions;

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
            //this is only eventsource test 
            //var sub = new SubPayload(customer.Id, customer.Name);
            //var payload = new Payload
            //{
            //    Sub = sub,
            //    KeyValuePairs = new Dictionary<Guid, SubPayload> { { customer.Id, sub } },
            //    SubPayloads = new List<SubPayload> { sub }
            //};
            //DatabaseEventSource.Instance.PayloadHad(payload);
            _customerAppService.Register(customer);
            //DatabaseEventSource.Instance.RegisterComplete();
        }

        /// <summary>
        /// 7246784B-782D-4B9F-965E-E2FD3547FC90
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public CustomerViewModel GetCustomer(Guid id)
        {
            //this is only eventsource test 
            //DatabaseEventSource.Instance.OnCammandExecute(2, "this is a sql");
            //DiagnosticObserver.Instance.RegisteDiagnosticObserver();
            return _customerAppService.GetById(id);
        }
    }
}
