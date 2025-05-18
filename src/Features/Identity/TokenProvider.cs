using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_qrshop.Features.Identity;

public class TokenProvider(UserManager<ApplicationUser> _userManager,
                           IOptions<JwtSettings> jwtOptions) : ITokenProvider
{
  private readonly JwtSettings _jwtSettings = jwtOptions.Value;
  public async Task<string> Create(ApplicationUser user)
  {
    var userClaims = await _userManager.GetClaimsAsync(user);
    var roles = await _userManager.GetRolesAsync(user);

    var userRoles = roles.Select(r => new Claim(ClaimTypes.Role, r));

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
      }
    .Union(userClaims)
    .Union(userRoles);

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _jwtSettings.Issuer,
      audience: _jwtSettings.Audience,
      claims: claims,
      expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
      signingCredentials: signingCredentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
