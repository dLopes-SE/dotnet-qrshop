namespace dotnet_qrshop.Common.Models.Identity;

public class JwtSettings
{
  public string Key { get; set; }
  public string Issuer { get; set; }
  public string Audience { get; set; }
  public int ExpirationInMinutes { get; set; }
}
