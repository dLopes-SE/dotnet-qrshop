﻿namespace dotnet_qrshop.Common.Models.Identity;

public class JwtSettings
{
  public string Key { get; set; } = string.Empty;
  public string Issuer { get; set; } = string.Empty;
  public string Audience { get; set; } = string.Empty;
  public int ExpirationInMinutes { get; set; }
}
