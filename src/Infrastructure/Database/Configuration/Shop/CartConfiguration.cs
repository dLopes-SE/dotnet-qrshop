using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Shop;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
  public void Configure(EntityTypeBuilder<Cart> builder)
  {
    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
      .UseIdentityColumn();

    builder.HasIndex(c => c.Id);

    builder.Property(c => c.VersionHash)
      .HasMaxLength(128);

    builder.HasOne(c => c.User)
      .WithOne()
      .HasForeignKey<Cart>(c => c.UserId);

    builder.HasMany(c => c.Items)
      .WithOne(ci => ci.Cart)
      .HasForeignKey(ci => ci.CartId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
