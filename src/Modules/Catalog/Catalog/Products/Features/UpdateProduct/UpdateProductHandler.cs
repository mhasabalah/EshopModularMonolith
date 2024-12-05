namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(e => e.Product.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(e => e.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(e => e.Product.Price).GreaterThan(0).WithMessage("Price must grater than 0");
    }
}

public class UpdateProductHandler(ICatalogRepository productRepository)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product? product =
            await productRepository.GetByIdAsync(command.Product.Id, cancellationToken: cancellationToken);
        if (product == null) throw new ProductNotFoundException(command.Product.Id);

        product.Update(
            command.Product.Name,
            command.Product.Category,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Price
        );

        await productRepository.UpdateAsync(product, cancellationToken);

        return new UpdateProductResult(true);
    }
}