using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

// public record GetProductsResponse(IEnumerable<ProductDto> Products);
public record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                GetProductsResult result = await sender.Send(new GetProductsQuery(request));
                GetProductsResponse response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get all products")
            .WithDescription("Get all products");
    }
}