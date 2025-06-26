using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Abstractions.Authentication;

public interface ITokenProvider
{
  Task<string> Create(ApplicationUser user);
}
