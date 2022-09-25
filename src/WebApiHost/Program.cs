namespace WebApiHost;

public class Program
{
    public static void Main(string[] args)
    {
        // This code for Serilog&LogDashboard ,It aslo can be used in DDD project!
        string logOutputTemplate = "{Timestamp:HH:mm:ss.fff zzz} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}";
        Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Default", LogEventLevel.Information)
          .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
          .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
          .Enrich.FromLogContext()
          .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
          .WriteTo.File($"{AppContext.BaseDirectory}Logs/serilog.log", rollingInterval: RollingInterval.Day, outputTemplate: logOutputTemplate)
          .CreateLogger();

        var host = CreateHostBuilder(args).Build();
        try
        {
            //see IWebHostExtensions in https://github.com/dotnet-architecture/eShopOnContainers/
            host.MigrateDbContext<CustomerContext>((_, __) => { })
               .MigrateDbContext<EventStoreSQLContext>((_, __) => { });
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Program terminated unexpectedly!");
            throw;
        }
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
