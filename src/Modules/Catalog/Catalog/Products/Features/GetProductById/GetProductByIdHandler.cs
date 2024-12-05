namespace Catalog.Products.Features.GetProductById;

public class GetProductByIdHandler(ICatalogRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByIdAsync(query.Id, true, cancellationToken);

        if (product == null) throw new ArgumentNullException(nameof(Product));

        ProductDto productDto = product.Adapt<ProductDto>();

        return new GetProductByIdResult(productDto);
    }
}