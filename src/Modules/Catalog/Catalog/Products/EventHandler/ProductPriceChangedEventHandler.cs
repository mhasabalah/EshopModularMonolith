namespace Catalog.Products.EventHandler;

public class ProductPriceChangedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    : INotificationHandler<ProductPriceChangedEvent>
{
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Product price changed: {Product}", notification.Product);

        return Task.CompletedTask;
    }
}
