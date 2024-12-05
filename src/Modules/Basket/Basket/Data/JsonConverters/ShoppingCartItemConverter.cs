namespace Basket.Data.JsonConverters;

public class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        JsonElement rootElement = jsonDocument.RootElement;

        Guid id = rootElement.GetProperty("id").GetGuid();
        Guid shoppingCartId = rootElement.GetProperty("shoppingCartId").GetGuid();
        Guid productId = rootElement.GetProperty("productId").GetGuid();
        int quantity = rootElement.GetProperty("quantity").GetInt32();
        string color = rootElement.GetProperty("color").GetString()!;
        decimal price = rootElement.GetProperty("price").GetDecimal();
        string productName = rootElement.GetProperty("productName").GetString()!;

        return new ShoppingCartItem(id, shoppingCartId, productId, quantity, color, price, productName);
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("shoppingCartId", value.ShoppingCartId.ToString());
        writer.WriteString("productId", value.ProductId.ToString());
        writer.WriteNumber("quantity", value.Quantity);
        writer.WriteString("color", value.Color);
        writer.WriteNumber("price", value.Price);
        writer.WriteString("productName", value.ProductName);

        writer.WriteEndObject();
    }
}