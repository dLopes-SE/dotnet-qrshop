﻿using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Common.Models.Identity;

public record AuthResponse (Guid Id, string Email, string Token)
{
  public static AuthResponse Parse(ApplicationUser user, string? token = null) =>
    new(user.Id, user.Email, token ?? string.Empty);
}
