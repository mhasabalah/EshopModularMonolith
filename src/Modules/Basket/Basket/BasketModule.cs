namespace Basket;

public static class BasketModule
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services,
                                                      IConfiguration configuration)
    {
        // Register services for the Basket module here
        // Example: services.AddTransient<IBasketService, BasketService>();
        return services;
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        // Configure middleware for the Basket module here
        // Example: app.UseMiddleware<BasketMiddleware>();
        return app;
    }
}
