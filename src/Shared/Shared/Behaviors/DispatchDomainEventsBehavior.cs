namespace Shared.Behaviors;

public class DispatchDomainEventsBehavior<TRequest, TResponse>(IMediator mediator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TRequest>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        List<IAggregate> aggregateRoots = request.GetType().GetProperties()
            .Where(p => p.PropertyType.GetInterfaces().Contains(typeof(IAggregate)))
            .Select(p => p.GetValue(request))
            .Cast<IAggregate>()
            .ToList();

        List<IDomainEvent> domainEvents = aggregateRoots
            .SelectMany(a => a.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(a => a.ClearDomainEvents());

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }

        return await next();
    }
}