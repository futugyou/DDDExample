namespace WebApiHost;

public static class SerilogExtensions
{
    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
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

        builder.Host.UseSerilog();
    }
}
