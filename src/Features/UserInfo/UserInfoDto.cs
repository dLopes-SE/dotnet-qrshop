using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.UserInfo;

public record UserInfoDto(string Name,
  string Email,
  string PhoneNumber,
  IEnumerable<AddressDto> Addresses)
{
  public static explicit operator UserInfoDto(ApplicationUser user) => new(user.Name,
    user.Email ?? string.Empty,
    user.PhoneNumber ?? string.Empty,
    user.Addresses
      .OrderByDescending(a => a.IsFavourite)
      .Select(a => (AddressDto)a));
}
