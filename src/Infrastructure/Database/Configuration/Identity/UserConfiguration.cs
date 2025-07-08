using dotnet_qrshop.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_qrshop.Infrastructure.Database.Configuration.Identity;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
  public void Configure(EntityTypeBuilder<ApplicationUser> builder)
  {
    builder.HasData(
      new ApplicationUser
      {
        Id = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
        Email = "admin@localhost.com",
        NormalizedEmail = "ADMIN@LOCALHOST.COM",
        Name = "System Admin",
        UserName = "admin@localhost.com",
        NormalizedUserName = "ADMIN@LOCALHOST.COM",
        PasswordHash = "AQAAAAIAAYagAAAAEH1nkGC0NgsEVoDkJH7tzlS7s0MkO91QcDinx/E0ENy4v/Q7srHiwnJmMyUiXOVV6Q==",
        ConcurrencyStamp = "04f197f7-1f0a-41f9-91f1-b7933076cf0b",
        SecurityStamp = "00dc5147-c14c-47df-8c69-31e97241aae2",
        EmailConfirmed = true
      },
      new ApplicationUser
      {
        Id = new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
        Email = "user@localhost.com",
        NormalizedEmail = "USER@LOCALHOST.COM",
        Name = "System User",
        UserName = "user@localhost.com",
        NormalizedUserName = "USER@LOCALHOST.COM",
        PasswordHash = "AQAAAAIAAYagAAAAEH1nkGC0NgsEVoDkJH7tzlS7s0MkO91QcDinx/E0ENy4v/Q7srHiwnJmMyUiXOVV6Q==",
        ConcurrencyStamp = "36614143-1fd7-4414-ac79-e5c38a46c1d9",
        SecurityStamp = "2ed6dccd-c411-4f1a-9c2d-a71a3812ad3b",
        EmailConfirmed = true
      }
    );
  }
}
