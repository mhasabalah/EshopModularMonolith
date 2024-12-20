﻿namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                GetProductByCategoryResult result = await sender.Send(new GetProductByCategoryQuery(category));
                GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
    }
}