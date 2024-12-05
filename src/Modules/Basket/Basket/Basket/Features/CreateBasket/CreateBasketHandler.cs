namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");
    }
}

public class CreateBasketHandler(IBasketRepository repository)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart basket = CreateBasket(command.ShoppingCart);

        await repository.CreateBasket(basket, cancellationToken);

        return new CreateBasketResult(basket.Id);
    }

    private static ShoppingCart CreateBasket(ShoppingCartDto shoppingCart)
    {
        ShoppingCart basket = ShoppingCart.Create(
            new Guid(), shoppingCart.UserName);

        foreach (ShoppingCartItemDto item in shoppingCart.Items)
        {
            basket.AddItem(
                item.ProductId,
                item.Quantity,
                item.Price,
                item.Color,
                item.ProductName
            );
        }

        return basket;
    }
}