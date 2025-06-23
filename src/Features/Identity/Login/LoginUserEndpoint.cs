using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models.Identity;

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
      return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error); // TODO DYLAN: CREATE ResultExtension for result match  
    }).WithName("LoginUser");
  }
}
