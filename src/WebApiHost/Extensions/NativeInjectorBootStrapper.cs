namespace WebApiHost;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        var mySqlServerVersion = config["MysqlVersion"];
        if (string.IsNullOrWhiteSpace(mySqlServerVersion))
        {
            throw new ArgumentException("The configuration value for 'MysqlVersion' is null or whitespace.", nameof(config));
        }
        //application
        services.AddScoped<ICustomerAppService, CustomerAppService>();

        //command bus
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Domain - Commands
        services.AddScoped<IRequestHandler<RegisterCustomerCommand>, CustomerCommandHandler>();

        // Domain - Events
        //services.AddScoped<INotificationHandler<CustomerRegisterEvent>, CustomerEventHandler>();

        // Domain - Notification
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        //infrastruct
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        var serverVersion = new MySqlServerVersion(new Version(mySqlServerVersion));
        services.AddDbContextPool<CustomerContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(config.GetConnectionString("Default"), serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Event Sourcing
        services.AddScoped(typeof(IEventStoreRepository<>), typeof(EventStoreRepository<>));
        services.AddScoped<IDomainEventRebuilder, DomainEventRebuilder>();
        services.AddScoped(typeof(IAggregateInvoker<>), typeof(AggregateInvoker<>));

        services.AddScoped<IEventSourcingDispatch, EventSourcingDispatch>();
        services.AddScoped(typeof(IEventSourcingHandler<>), typeof(EventSourcingHandler<>));

    }
}
