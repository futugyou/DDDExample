using System;
using Example.Application.Interfaces;
using Example.Application.Services;
using Example.Domain.CommandHandlers;
using Example.Domain.Commands.Customer;
using Example.Domain.Core.Bus;
using Example.Domain.Core.Events;
using Example.Domain.Core.Notifications;
using Example.Domain.EventHandlers;
using Example.Domain.Events.Customer;
using Example.Domain.Interfaces;
using Example.Infrastruct.Data.Bus;
using Example.Infrastruct.Data.Context;
using Example.Infrastruct.Data.EventStore;
using Example.Infrastruct.Data.Repository;
using Example.Infrastruct.Data.Repository.EventSourcing;
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

            // Domain - Events
            services.AddScoped<INotificationHandler<CustomerRegisterEvent>, CustomerEventHandler>();

            // Domain - Notification
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //infrastruct
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //ÊÂ¼þËÝÔ´
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<EventStoreSQLContext>();
            services.AddScoped<IEventStore, SqlEventStore>();

        }
    }
}
