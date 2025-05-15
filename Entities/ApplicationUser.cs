using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Entities;

public class ApplicationUser : IdentityUser
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
