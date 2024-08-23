﻿var appName = "ddd-demo";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
var openTelemetryBuilder = services.AddOpenTelemetry();
var serviceName = configuration.GetValue<string>("Jaeger:ServiceName");
if (!string.IsNullOrWhiteSpace(serviceName))
{
    openTelemetryBuilder = openTelemetryBuilder.WithTracing((builder) =>
    {
        _ = builder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddBaggageActivityProcessor()
        .AddEntityFrameworkCoreInstrumentation(config => config.SetDbStatementForText = true)
        .AddJaegerExporter(config =>
        {
            configuration.GetSection("Jaeger").Bind(config);
        })
        .AddOtlpExporter(option =>
        {
            option.Endpoint = new Uri(configuration["Honeycomb:Endpoint"]!);
            option.Headers = $"x-honeycomb-team={configuration["Honeycomb:ApiKey"]!}";
        });
    });
}

//it doesn't work ,so use 'Bind'
services.Configure<JaegerExporterOptions>(configuration.GetSection("Jaeger"));
services.AddLogDashboard();
//启动配置   
services.AddAutoMapperSetup();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD project", Version = "v1" });
});
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.RegisterServicesFromAssemblyContaining<CommandHandler>();
});

NativeInjectorBootStrapper.RegisterServices(services, configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD project v1"));
}

app.UseRouting();
app.UseLogDashboard();
app.UseAuthorization();

app.MapControllers();

try
{
    app.Logger.LogInformation("Applying database migration ({ApplicationName})...", appName);
    app.ApplyDatabaseMigration<CustomerContext>((_, _) => { });

    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);
}
finally
{
    Log.CloseAndFlush();
}
