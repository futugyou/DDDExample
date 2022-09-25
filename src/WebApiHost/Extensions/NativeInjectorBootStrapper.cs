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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace WebApiHost.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
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

            var serverVersion = new MySqlServerVersion(new Version(config["MysqlVersion"]));
            services.AddDbContextPool<CustomerContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(config.GetConnectionString("Default"), serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //ÊÂ¼þËÝÔ´
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddDbContextPool<EventStoreSQLContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(config.GetConnectionString("Default"), serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            services.AddScoped<IEventStore, SqlEventStore>();

        }
    }
}
