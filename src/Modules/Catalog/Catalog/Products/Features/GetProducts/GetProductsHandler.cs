namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<ProductDto> Products);

public class GetProductsHandler(IProductRepository _productRepository) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product>? products = await _productRepository.GetAllAsync(cancellationToken);

        IEnumerable<ProductDto> productDtos = products?.Adapt<IEnumerable<ProductDto>>() ?? Enumerable.Empty<ProductDto>();


        return new GetProductsResult(productDtos);
    }
}
