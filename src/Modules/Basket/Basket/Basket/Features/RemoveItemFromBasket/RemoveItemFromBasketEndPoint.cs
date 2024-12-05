namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketResponse(Guid ShoppingCartId);

public class RemoveItemFromBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}/items/{productId}",
                async ([FromRoute] string userName, [FromRoute] Guid productId, ISender sender) =>
                {
                    RemoveItemFromBasketResult result =
                        await sender.Send(new RemoveItemFromBasketCommand(userName, productId));

                    RemoveItemFromBasketResponse response = result.Adapt<RemoveItemFromBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("RemoveItemFromBasket")
            .Produces<RemoveItemFromBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Remove Item From Basket")
            .WithDescription("Remove Item From Basket");
    }
}