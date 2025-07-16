using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Logout;

public class LogoutUserEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/logout", async (
      ICommandHandler<LogoutUserCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new LogoutUserCommand(), cancellationToken);
      return result.Match(() => Results.Ok(), CustomResults.Problem);
    });
  }
}
