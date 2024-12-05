namespace Basket.Basket.Models;

public class ShoppingCartItem : Entity<Guid>
{
    internal ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, string color, decimal price,
        string productName)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    [JsonConstructor]
    public ShoppingCartItem(Guid id, Guid shoppingCartId, Guid productId, int quantity, string color, decimal price,
        string productName)
    {
        Id = id;
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    public Guid ShoppingCartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; internal set; }
    public string Color { get; private set; } = default!;

    // will come from Catalog module
    public decimal Price { get; private set; }
    public string ProductName { get; private set; } = default!;

    public void AddQuantity(int quantity) => Quantity += quantity;
    public void SetQuantity(int quantity) => Quantity = quantity;

    public void UpdatePrice(decimal newPrice)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newPrice);
        Price = newPrice;
    }
}