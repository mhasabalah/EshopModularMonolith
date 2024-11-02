namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDto Product);

public class GetProductByIdHandler(IProductRepository _productRepository) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id,true, cancellationToken);

        if (product == null)
            throw new ArgumentNullException(nameof(Product));

        var productDto = product.Adapt<ProductDto>();

        return new GetProductByIdResult(productDto);
    }
}
