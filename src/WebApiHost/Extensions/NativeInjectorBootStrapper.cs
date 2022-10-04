namespace WebApiHost;

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
        //TODO replace to new IEventStoreRepository
        services.AddScoped<IEventStoreRepository, EventStoreRepository>();

    }
}
