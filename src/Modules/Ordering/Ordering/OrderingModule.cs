namespace Ordering;

public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register services for the Ordering module here
        // Example: services.AddTransient<IOrderingService, OrderingService>();
        return services;
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        // Configure middleware for the Ordering module here
        // Example: app.UseMiddleware<OrderingMiddleware>();
        return app;
    }
}