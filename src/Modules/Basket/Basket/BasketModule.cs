namespace Basket;

public static class BasketModule
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // add services to the container here
        // add Endpoint services
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CashedBasketRepository>();
        // add use case services

        services.AddDbContext<BasketDbContext>(configuration);
        // services.AddScoped<IDataSeeder, BasketDataSeeder>();

        return services;
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        // add Endpoint services

        // add use case services

        // add infrastructure services
        app.UseMigration<BasketDbContext>();

        return app;
    }
}