﻿using dotnet_qrshop.Domains;
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

    builder.HasData(
      new Cart
      {
        Id = 1,
        UserId = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
        VersionHash = "cZJh3Nz4RrpsK8I1MfMO/mxodc8rwZbWNju8sG9TZU0=",
        CreatedAt = new DateTime(2025, 06, 26, 13, 03, 14, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2025, 06, 26, 13, 03, 14, DateTimeKind.Utc)
      },
      new Cart
      {
        Id = 2,
        UserId = new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
        VersionHash = "33xgosi+0wuEyntDGEexeC57A8BK+3+QSPe1w0hW5DU=",
        CreatedAt = new DateTime(2025, 06, 26, 13, 03, 14, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2025, 06, 26, 13, 03, 14, DateTimeKind.Utc)
      }
    );
  }
}
