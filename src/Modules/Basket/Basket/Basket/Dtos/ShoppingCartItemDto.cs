namespace Basket.Basket.Dtos;

public record ShoppingCartItemDto(
    Guid Id,
    Guid ProductId,
    Guid ShoppingCartId,
    string ProductName,
    decimal Price,
    string Color,
    int Quantity
);