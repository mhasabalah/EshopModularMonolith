namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
public class CreateProductHandler(IProductRepository _productRepository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            command.Product.Name,
            command.Product.Category,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Price
        );

        await _productRepository.AddAsync(product, cancellationToken);

        return new CreateProductResult(product.Id);
    }
}