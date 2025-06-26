using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Identity.Register;

public class RegisterUserEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/register", async (
      RegistrationRequest request,
      ICommandHandler<RegisterUserCommand, RegistrationResponse> handler,
      CancellationToken cancellationToken) =>
    {
      var command = new RegisterUserCommand(request);

      var result = await handler.Handle(command, cancellationToken);

      return result.Match(Results.Ok, CustomResults.Problem);
    }).WithName("RegisterUser");
  }
}
  