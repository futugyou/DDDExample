using System;
using Example.Application.Interfaces;
using Example.Application.Services;
using Example.Domain.CommandHandlers;
using Example.Domain.Commands.Customer;
using Example.Domain.Core.Bus;
using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Bus;
using Example.Infrastruct.Data.Context;
using Example.Infrastruct.Data.Repository;
using Example.Infrastruct.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiHost.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //application
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            //command bus
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterCustomerCommand>, CustomerCommandHandler>();

            //infrastruct
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
