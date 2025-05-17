using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Abstractions;

public interface IAuthService
{
  Task<Result<AuthResponse>> Login (AuthRequest request);
  Task<Result<AuthResponse>> Registration(RegistrationRequest request);
}
