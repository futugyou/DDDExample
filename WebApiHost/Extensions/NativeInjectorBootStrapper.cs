using System;
using Example.Application.Interfaces;
using Example.Application.Services;
using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Context;
using Example.Infrastruct.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiHost.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
        }
    }
}
