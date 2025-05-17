using Carter;
using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Models.Identity;

namespace dotnet_qrshop.Features.Identity.Register;

public class RegisterUserEndpoint : ICarterModule
{
  public sealed class Request
  {

  }

  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/register", async (
      RegistrationRequest request,
      ICommandHandler<RegisterUserCommand, RegistrationResponse> handler,
      CancellationToken cancellationToken) =>
    {
      var command = new RegisterUserCommand(request);

      var result = await handler.Handle(command, cancellationToken);

      return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error); // TODO DYLAN: CREATE ResultExtension for result match  
    }).WithName("RegisterUser");
  }
}
