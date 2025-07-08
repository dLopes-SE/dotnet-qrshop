using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Domains;

public class ApplicationUser : IdentityUser<Guid>
{
  public string Name { get; set; }
}
