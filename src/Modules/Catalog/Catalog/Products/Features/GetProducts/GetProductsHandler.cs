using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest Request) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);

public class GetProductsHandler(ICatalogRepository productRepository)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        PaginatedResult<Product> products = await productRepository.GetAllAsync(query.Request, cancellationToken);

        PaginatedResult<ProductDto> productDtos = products.Adapt<PaginatedResult<ProductDto>>();

        return new GetProductsResult(productDtos);
    }
}