namespace Basket.Basket.EventHandler;

public class ProductPriceChangedIntegrationEventHandler(
    ILogger<ProductPriceChangedIntegrationEventHandler> logger,
    ISender sender) : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        logger.LogInformation("Integration event received: {Product}", context.Message);

        UpdateItemPriceInBasketCommand command = new(context.Message.ProductId, context.Message.Price);

        UpdateItemPriceInBasketResult result = await sender.Send(command, context.CancellationToken);

        if (!result.IsSuccess) logger.LogError("Product price update failed: {Product}", context.Message);

        logger.LogInformation("Product price updated: {Product}", context.Message);
    }
}