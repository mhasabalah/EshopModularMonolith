namespace Basket.Data.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProductId)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Color)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Quantity)
            .IsRequired();

        builder.Property(e => e.ProductName).IsRequired();
    }
}