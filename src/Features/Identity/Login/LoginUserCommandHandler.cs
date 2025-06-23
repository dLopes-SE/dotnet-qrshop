using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Login;

public class LoginUserCommandHandler(IAuthService _authService)
  : ICommandHandler<LoginUserCommand, AuthResponse>
{
  public async Task<Result<AuthResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
  {
    var result = await _authService.Login(command.Request);
    if (result.IsFailure)
    {
      return result;
    }
    return result;
  }
}
