namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketRequest(string UserName, ShoppingCartItemDto Item);

public record AddItemIntoBasketResponse(Guid Id);

public class AddItemIntoBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/{userName}/items",
                async ([FromRoute] string userName, [FromBody] AddItemIntoBasketRequest request, ISender sender) =>
                {
                    AddItemIntoBasketCommand command = new(userName, request.Item);

                    AddItemIntoBasketResult result = await sender.Send(command);

                    AddItemIntoBasketResponse response = result.Adapt<AddItemIntoBasketResponse>();

                    return Results.Created($"/basket/items/{response.Id}", response);
                })
            .WithName("AddItemIntoBasket")
            .Produces<AddItemIntoBasketResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Add an item into the basket.")
            .WithSummary("Add an item into the basket.");
    }
}