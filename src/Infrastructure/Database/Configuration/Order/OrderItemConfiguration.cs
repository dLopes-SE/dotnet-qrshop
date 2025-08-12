using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Order;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.HasKey(oi => oi.Id);

    builder.Property(oi => oi.Id)
      .UseIdentityColumn();

    builder.HasOne(oi => oi.Order)
      .WithMany(c => c.Items)
      .HasForeignKey(oi => oi.OrderId);

    builder.HasOne(oi => oi.Item)
      .WithMany(i => i.OrderItems);

    builder.Property(oi => oi.Quantity)
      .IsRequired();
  }
}
