namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDto ShoppingCart);

public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        ShoppingCart basket = await repository.GetBasket(query.UserName, cancellationToken: cancellationToken);

        ShoppingCartDto result = basket.Adapt<ShoppingCartDto>();

        return new GetBasketResult(result);
    }
}