using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Order;

public class OrderConfiguration : IEntityTypeConfiguration<Domains.Order>
{
  public void Configure(EntityTypeBuilder<Domains.Order> builder)
  {
    builder.HasKey(o => o.Id);

    builder.Property(o => o.Id)
      .UseIdentityColumn();

    builder.HasIndex(o => o.Id);

    builder.Property(o => o.VersionHash)
      .HasMaxLength(128);

    builder.HasOne(o => o.User)
      .WithOne()
      .HasForeignKey<Domains.Order>(o => o.UserId);

    builder.HasMany(o => o.Items)
      .WithOne(oi => oi.Order);
  }
}
