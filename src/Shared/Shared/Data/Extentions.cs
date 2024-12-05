using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Shared.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services,
        IConfiguration configuration)
        where TContext : DbContext
    {
        string? connectionString = configuration.GetConnectionString("Database");


        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<TContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        return services;
    }

    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
        SeedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }

    public static async Task MigrateDatabaseAsync<TContext>(this IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        TContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        await dbContext.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IEnumerable<IDataSeeder> seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (IDataSeeder seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}