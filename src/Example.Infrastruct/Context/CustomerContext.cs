namespace Example.Infrastruct;

public class CustomerContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventStore> EventStores { get; set; }

    protected CustomerContext()
    {
    }

    public CustomerContext(DbContextOptions<CustomerContext> options)
      : base(options)
    {
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        ChangeTracker.Tracked += ChangeTracker_Tracked;
    }

    private void ChangeTracker_Tracked(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
    {

    }

    private void ChangeTracker_StateChanged(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new CustomerMap());
        _ = modelBuilder.ApplyConfiguration(new EventStoreMap());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// 重写连接数据库
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 从 appsetting.json 中获取配置信息
        //var config = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        // 定义要使用的数据库
        //var serverVersion = new MySqlServerVersion(new Version(config["MysqlVersion"]));
        //optionsBuilder.UseMySql(config.GetConnectionString("Default"), serverVersion);
        //optionsBuilder.UseSqlite("Data Source = ddd_demo.db");
        //optionsBuilder.AddInterceptors(_commandInterceptor, _connectInterceptor, _transactionInterceptor);
    }

    //private static readonly TaggedQueryCommandInterceptor _commandInterceptor = new TaggedQueryCommandInterceptor();
    //private static readonly TaggedConnectionInterceptor _connectInterceptor = new TaggedConnectionInterceptor();
    //private static readonly TaggedTransactionInterceptor _transactionInterceptor = new TaggedTransactionInterceptor();
}
