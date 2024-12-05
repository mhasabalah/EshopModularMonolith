namespace Shared.Extensions;

public static class MediatRExtentions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            // config.AddOpenBehavior(typeof(TransactionBehavior<,>));
            // config.AddOpenBehavior(typeof(DispatchDomainEventsBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }
}