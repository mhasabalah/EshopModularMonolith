namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);
public class UpdateProductHandler(IProductRepository _productRepository)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Product.Id, cancellationToken: cancellationToken);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with Id {command.Product.Id} not found.");
        }

        product.Update(
            command.Product.Name,
            command.Product.Category,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Price
        );

        await _productRepository.UpdateAsync(product, cancellationToken);

        return new UpdateProductResult(true);
    }

}
