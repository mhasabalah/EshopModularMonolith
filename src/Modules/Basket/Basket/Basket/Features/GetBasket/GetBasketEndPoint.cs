namespace Basket.Basket.Features.GetBasket;

public record GetBasketResponse(ShoppingCartDto ShoppingCart);

public class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async ([FromRoute] string userName, ISender sender) =>
            {
                GetBasketResult result = await sender.Send(new GetBasketQuery(userName));

                GetBasketResponse response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
    }
}