using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Identity;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
  public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
  {
    builder.HasData(
      new IdentityRole<Guid>
      {
        Id = new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf"),
        Name = "User",
        NormalizedName = "USER"
      },
      new IdentityRole<Guid>
      {
        Id = new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"),
        Name = "Administrator",
        NormalizedName = "ADMINISTRATOR"
      }
    );
  }
}
