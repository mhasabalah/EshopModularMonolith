namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,
                                                      IConfiguration configuration)
    {
        // Register services for the Catalog module here
        // Example: services.AddTransient<ICatalogService, CatalogService>();
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        // Configure middleware for the Catalog module here
        // Example: app.UseMiddleware<CatalogMiddleware>();
        return app;
    }
}
