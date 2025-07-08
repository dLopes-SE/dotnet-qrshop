using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Shop;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
  public void Configure(EntityTypeBuilder<CartItem> builder)
  {
    builder.HasKey(ci => ci.Id);

    builder.Property(ci => ci.Id)
      .UseIdentityColumn();

    builder.HasOne(ci => ci.Cart)
      .WithMany(c => c.Items)
      .HasForeignKey(ci => ci.CartId);

    builder.HasOne(ci => ci.Item)
      .WithMany(i => i.CartItems)
      .HasForeignKey(ci => ci.ItemId);

    builder.Property(ci => ci.Quantity)
      .IsRequired();
  }
}
