using dotnet_qrshop.Entities;
using dotnet_qrshop.Persistence.Configurations.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Persistence.DbContext;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    
    builder.ApplyConfiguration(new UserConfiguration());
    builder.ApplyConfiguration(new RoleConfiguration());
    builder.ApplyConfiguration(new UserRoleConfiguration());
  }
}
