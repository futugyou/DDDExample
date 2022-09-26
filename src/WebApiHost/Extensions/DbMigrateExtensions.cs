namespace WebApiHost;

public static class DbMigrateExtensions
{
    public static void ApplyDatabaseMigration<TContext>(this WebApplication app, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

        try
        {
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(new TimeSpan[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(8),
                });

            //if the sql server container is not created on run docker compose this
            //migration can't fail for network related exception. The retry options for DbContext only 
            //apply to transient exceptions
            // Note that this is NOT applied when running some orchestrators (let the orchestrator to recreate the failing service)
            retry.Execute(() => InvokeSeeder(seeder, context, services));

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
        }
    }
    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
       where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}
