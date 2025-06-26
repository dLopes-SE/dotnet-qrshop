using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Shop;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
  public void Configure(EntityTypeBuilder<Item> builder)
  {
    builder.HasKey(i => i.Id);
    builder.Property(i => i.Id)
      .UseIdentityColumn();

    builder.Property(i => i.Name)
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(i => i.Slogan)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(i => i.Description)
      .IsRequired()
      .HasMaxLength(500);

    builder.Property(i => i.Image)
     .IsRequired();

    builder.Property(i => i.IsFeaturedItem)
      .IsRequired()
      .HasDefaultValue(false)
      .HasColumnName("is_featured_item");
  }
}
