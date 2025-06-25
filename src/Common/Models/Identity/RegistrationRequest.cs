using System.ComponentModel.DataAnnotations;

namespace dotnet_qrshop.Common.Models.Identity;

public class RegistrationRequest
{
  [Required]
  public string Name { get; set; }

  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MinLength(6)]
  public string Password { get; set; }
}