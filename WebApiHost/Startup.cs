using LogDashboard;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WebApiHost.Extensions;

namespace WebApiHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenTelemetryTracing((builder) =>
            {
                builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ddd-demo"))
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddJaegerExporter();
            });
            services.AddLogDashboard();
            //启动配置   
            services.AddAutoMapperSetup();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD project", Version = "v1" });
            });
            services.AddMediatR(typeof(Startup), typeof(Example.Domain.CommandHandlers.CommandHandler));

            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD project v1"));
            }
            app.UseRouting();
            app.UseLogDashboard();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
