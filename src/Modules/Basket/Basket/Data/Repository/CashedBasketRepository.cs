namespace Basket.Data.Repository;

public class CashedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = {new ShoppingCartConverter(), new ShoppingCartItemConverter()}
    };

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        string basketCacheKey = $"Basket-{userName}";

        if (!asNoTracking)
            return await repository.GetBasket(userName, asNoTracking, cancellationToken);

        string? basket = await cache.GetStringAsync(basketCacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(basket))
            return JsonSerializer.Deserialize<ShoppingCart>(basket, _options);

        ShoppingCart result = await repository.GetBasket(userName, asNoTracking, cancellationToken);

        await cache.SetStringAsync(basketCacheKey, JsonSerializer.Serialize(result, _options), cancellationToken);

        return result;
    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        string basketCacheKey = $"Basket-{basket.UserName}";

        await cache.SetStringAsync(basketCacheKey, JsonSerializer.Serialize(basket, _options), cancellationToken);

        return await repository.CreateBasket(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        string basketCacheKey = $"Basket-{userName}";

        await cache.RemoveAsync(basketCacheKey, cancellationToken);

        return await repository.DeleteBasket(userName, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        if (userName is null)
            return await repository.SaveChangesAsync(userName, cancellationToken);

        string basketCacheKey = $"Basket-{userName}";
        await cache.RemoveAsync(basketCacheKey, cancellationToken);
        return await repository.SaveChangesAsync(userName, cancellationToken);
    }
}