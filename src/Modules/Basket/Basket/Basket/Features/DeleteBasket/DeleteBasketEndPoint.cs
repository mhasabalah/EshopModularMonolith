namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async ([FromRoute] string userName, ISender sender) =>
            {
                DeleteBasketResult result = await sender.Send(new DeleteBasketCommand(userName));

                DeleteBasketResponse response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");
    }
}