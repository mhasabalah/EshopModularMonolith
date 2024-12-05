namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(e => e.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(e => e.Product.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(e => e.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(e => e.Product.Price).GreaterThan(0).WithMessage("Price must grater than 0");
    }
}

public class CreateProductHandler(ICatalogRepository productRepository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = Product.Create(
            Guid.NewGuid(),
            command.Product.Name,
            command.Product.Category,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Price
        );

        await productRepository.AddAsync(product, cancellationToken);

        return new CreateProductResult(product.Id);
    }
}