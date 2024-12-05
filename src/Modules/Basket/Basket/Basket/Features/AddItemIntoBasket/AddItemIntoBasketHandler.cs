using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto Item)
    : ICommand<AddItemIntoBasketResult>;

public record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.Item.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Item.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than 0.");
    }
}

public class AddItemIntoBasketHandler(IBasketRepository repository, ISender sender)
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command,
        CancellationToken cancellationToken)
    {
        ShoppingCart basket = await repository.GetBasket(command.UserName, false, cancellationToken);
        GetProductByIdResult result =
            await sender.Send(new GetProductByIdQuery(command.Item.ProductId), cancellationToken);

        basket.AddItem(
            command.Item.ProductId,
            command.Item.Quantity,
            result.Product.Price,
            command.Item.Color,
            result.Product.Name
        );
        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult(basket.Id);
    }
}