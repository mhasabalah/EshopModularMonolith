namespace Basket.Data.JsonConverters;

public class ShoppingCartConverter : JsonConverter<ShoppingCart>
{
    public override ShoppingCart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        JsonElement rootElement = jsonDocument.RootElement;

        Guid id = rootElement.GetProperty("id").GetGuid();
        string userName = rootElement.GetProperty("userName").GetString()!;
        JsonElement itemsElement = rootElement.GetProperty("items");

        ShoppingCart shoppingCart = ShoppingCart.Create(id, userName);

        List<ShoppingCartItem>? items = itemsElement.Deserialize<List<ShoppingCartItem>>(options);
        if (items != null)
        {
            FieldInfo? itemsField =
                typeof(ShoppingCart).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            itemsField?.SetValue(shoppingCart, items);
        }

        return shoppingCart;
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCart value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("userName", value.UserName);

        writer.WritePropertyName("items");
        JsonSerializer.Serialize(writer, value.Items, options);

        writer.WriteEndObject();
    }
}