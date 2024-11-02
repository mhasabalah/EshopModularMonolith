using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,
                                                      IConfiguration configuration)
    {

        // add services to the container here

        // add Endpoint services

        // add use case services

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        // add infrastructure services
        //string? connectionString = configuration.GetConnectionString("Database");

        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //services.AddDbContext<CatalogDbContext>((sp, options) =>
        //{
        //    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        //    options.UseNpgsql(connectionString);
        //});
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