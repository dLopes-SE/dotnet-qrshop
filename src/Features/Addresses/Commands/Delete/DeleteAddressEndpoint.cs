using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Addresses.Commands.Delete;

public class DeleteAddressEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("user/address/{id}", async (
      int id,
      ICommandHandler<DeleteAddressCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle((DeleteAddressCommand)id, cancellationToken);

      return result.Match(() => Results.Ok(), CustomResults.Problem);
    })
      .WithName("RemoveAddress")
      .RequireAuthorization();
  }
}
