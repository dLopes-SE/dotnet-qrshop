using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Register;


public class RegisterUserCommandHandler(IAuthService _authService)
  : ICommandHandler<RegisterUserCommand, RegistrationResponse>
{
  public async Task<Result<RegistrationResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
  {
    var result = await _authService.Registration(command.Request);
    if (result.IsFailure)
    {
      return Result.Failure<RegistrationResponse>(result.Error);
    }

    return Result.Success(new RegistrationResponse
    {
      UserId = result.Value.Id,
    });
  }
}
