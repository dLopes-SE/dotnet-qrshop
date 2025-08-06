using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.User;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
  public void Configure(EntityTypeBuilder<Address> builder)
  {
    builder.HasKey(a => a.Id);

    builder.Property(a => a.Id)
      .UseIdentityColumn();

    builder.HasIndex(a => a.Id);

    builder.Property(a => a.FullName)
      .IsRequired();
    
    builder.Property(a => a.Phone)
      .IsRequired()
      .HasMaxLength(20);

    builder.Property(a => a.Address_line1)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(a => a.Address_line2)
      .HasMaxLength(200);

    builder.Property(a => a.PostalCode)
      .IsRequired()
      .HasMaxLength(25);

    builder.Property(a => a.City)
      .IsRequired()
      .HasMaxLength(255);

    builder.Property(a => a.State_or_Province)
        .HasMaxLength(100);

    builder.Property(a => a.Country)
        .HasMaxLength(2);

    builder.Property(a => a.IsFavourite)
      .HasDefaultValue(false);

    builder.HasOne(c => c.User)
      .WithMany(u => u.Addresses)
      .HasForeignKey(a => a.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
