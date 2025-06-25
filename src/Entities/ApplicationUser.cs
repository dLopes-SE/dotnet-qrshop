using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Entities;

public class ApplicationUser : IdentityUser
{
  public string Name { get; set; }
}
