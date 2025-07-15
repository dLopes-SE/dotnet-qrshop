using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Login;

public class LoginUserEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/login", async (
      AuthRequest request,
      ICommandHandler<LoginUserCommand, AuthResponse> handler,
      CancellationToken cancellationToken) =>
    {
      var command = new LoginUserCommand(request);

      var result = await handler.Handle(command, cancellationToken);
      return result.Match(Results.Ok, CustomResults.Problem);
    }).WithName("LoginUser");
  }
}
