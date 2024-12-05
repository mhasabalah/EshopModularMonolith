namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category)
    : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);

public class GetProductByCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
{
    public GetProductByCategoryQueryValidator()
    {
        RuleFor(x => x.Category).NotEmpty();
    }
}

public class GetProductByCategoryHandler(ICatalogRepository productRepository)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        IEnumerable<Product>? products =
            await productRepository.GetProductsByCategory(query.Category, cancellationToken);

        IEnumerable<ProductDto>? productsDto = products.Adapt<IEnumerable<ProductDto>>();

        return new GetProductByCategoryResult(productsDto);
    }
}