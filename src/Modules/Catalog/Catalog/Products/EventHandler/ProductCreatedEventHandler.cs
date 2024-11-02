namespace Catalog.Products.EventHandler;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) 
    : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Product created: {Product}", notification.Product);
        
        return Task.CompletedTask;
    }
}
