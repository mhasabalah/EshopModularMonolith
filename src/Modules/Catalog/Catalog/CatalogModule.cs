using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,
                                                      IConfiguration configuration)
    {

        // add services to the container here

        // add Endpoint services

        // add use case services

        // add infrastructure services
        services.AddDbContext<CatalogDbContext>(configuration);
        services.AddScoped<IDataSeeder, CatalogDataSeeder>();

        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {

        // add Endpoint services

        // add use case services

        // add infrastructure services
        app.UseMigration<CatalogDbContext>();


        return app;
    }
}
        //string? connectionString = configuration.GetConnectionString("Database");
        //services.AddDbContext<CatalogDbContext>(options =>
        //{
        //    options.UseNpgsql(connectionString);
        //});