using dotnet_qrshop.Domains;
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

    builder.Property(o => o.Status)
      .HasConversion<string>();

    builder.HasOne<Address>()
      .WithMany()
      .HasForeignKey(o => o.AddressId);

    builder.Property(o => o.FullName)
      .IsRequired();

    builder.Property(o => o.Phone)
      .IsRequired()
      .HasMaxLength(20);

    builder.Property(o => o.Address_line1)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(o => o.Address_line2)
      .HasMaxLength(200);

    builder.Property(o => o.PostalCode)
      .IsRequired()
      .HasMaxLength(25);

    builder.Property(o => o.City)
      .IsRequired()
      .HasMaxLength(255);

    builder.Property(o => o.State_or_Province)
        .HasMaxLength(100);

    builder.Property(o => o.Country)
        .HasMaxLength(2);

    builder.HasOne(o => o.User)
      .WithOne()
      .HasForeignKey<Domains.Order>(o => o.UserId);

    builder.HasMany(o => o.Items)
      .WithOne(oi => oi.Order);
  }
}
