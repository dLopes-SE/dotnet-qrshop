using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Domain;

public class ApplicationUser : IdentityUser
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
