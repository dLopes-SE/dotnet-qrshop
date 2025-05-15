using System.ComponentModel.DataAnnotations;

namespace dotnet_qrshop.Common.Models.Identity;

public class AuthRequest
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }
  [Required]
  public string Password { get; set; }
}
