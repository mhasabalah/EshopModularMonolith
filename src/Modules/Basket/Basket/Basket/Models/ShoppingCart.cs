namespace Basket.Basket.Models;

public class ShoppingCart : Aggregate<Guid>
{
    private readonly List<ShoppingCartItem> _items = new();
    public string UserName { get; private set; } = default!;
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public static ShoppingCart Create(Guid id, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        ShoppingCart cart = new()
        {
            Id = id,
            UserName = userName
        };
        return cart;
    }

    public void AddItem(Guid productId, int quantity, decimal price, string color, string productName)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        ShoppingCartItem? existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
            existingItem.AddQuantity(quantity);
        else
        {
            ShoppingCartItem item = new(Id, productId, quantity, color, price, productName);
            _items.Add(item);
        }
    }

    public void AddItem(ShoppingCartItem item)
    {
        ShoppingCartItem? existingItem = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existingItem != null)
            existingItem.AddQuantity(item.Quantity);
        else
            _items.Add(item);
    }

    public void RemoveItem(Guid productId)
    {
        ShoppingCartItem? item = Items.FirstOrDefault(x => x.ProductId == productId);
        if (item != null) _items.Remove(item);
    }

    public void ClearItems() => _items.Clear();

    public void SetQuantities(Dictionary<Guid, int> quantities)
    {
        foreach (ShoppingCartItem item in Items)
        {
            if (quantities.TryGetValue(item.ProductId, out int quantity))
                item.SetQuantity(quantity);
        }
    }
}