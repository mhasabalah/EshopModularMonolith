namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category)
    : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);
public class GetProductByCategoryHandler(IProductRepository _productRepository)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Product>? products = await _productRepository.GetProductsByCategory(query.Category, cancellationToken);

        IEnumerable<ProductDto>? productsDto = products.Adapt<IEnumerable<ProductDto>>();

        return new GetProductByCategoryResult(productsDto);
    }
}
