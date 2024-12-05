namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                DeleteProductResult result = await sender.Send(new DeleteProductCommand(id));
                DeleteProductResponse response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete a product")
            .WithDescription("Delete a product");
    }
}