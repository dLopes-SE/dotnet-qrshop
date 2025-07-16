using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Logout;

public class LogoutUserCommandHandler(
  IHttpContextAccessor _contextAccessor,
  IAuthService _authService) : ICommandHandler<LogoutUserCommand>
{
  public async Task<Result> Handle(LogoutUserCommand command, CancellationToken cancellationToken)
  {
    var httpContext = _contextAccessor.HttpContext;
    if (httpContext is null)
    {
      return Result.Failure(Error.Failure("Failed to obtain HttpContext", "Error getting cart, please try again or contact the support"));
    }

    if (httpContext.User?.Identity?.IsAuthenticated is not true)
    {
      return Result.Success();
    }

    httpContext.Response.DeleteCookie("auth");

    return await _authService.Logout();
  }
}
