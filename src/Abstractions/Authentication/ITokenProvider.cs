using dotnet_qrshop.Entities;

namespace dotnet_qrshop.Abstractions.Authentication;

public interface ITokenProvider
{
  Task<string> Create(ApplicationUser user);
}
