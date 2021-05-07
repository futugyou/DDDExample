using Example.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApiHost.Extensions;

namespace WebApiHost.Controllers
{
    /// <summary>
    /// THIS CONTROLLER ONLY FOR EVENTSOURCE DEMO ,NOT BELONG TO DDD PROJECT!
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsourceController : ControllerBase
    {
        [HttpPost]
        public void SaveCustomer(CustomerViewModel customer)
        {
            var sub = new SubPayload(customer.Id, customer.Name);
            var payload = new Payload
            {
                Sub = sub,
                KeyValuePairs = new Dictionary<Guid, SubPayload> { { customer.Id, sub } },
                SubPayloads = new List<SubPayload> { sub }
            };
            DatabaseEventSource.Instance.PayloadHad(payload);
            DatabaseEventSource.Instance.RegisterComplete();
        }

        [HttpGet]
        public void GetCustomer()
        {
            DatabaseEventSource.Instance.OnCammandExecute(2, "this is a sql");
            DiagnosticObserver.Instance.RegisteDiagnosticObserver();
        }
    }
}
