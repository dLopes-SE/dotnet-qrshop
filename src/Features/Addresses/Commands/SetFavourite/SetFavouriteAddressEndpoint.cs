using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Addresses.Commands.SetFavourite;

public class SetFavouriteAddressEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/address/{id}/favourite", async (
      int id,
      ICommandHandler<SetFavouriteAddressCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle((SetFavouriteAddressCommand)id, cancellationToken);

      return result.Match(Results.NoContent, CustomResults.Problem);
    })
      .WithName("SetFavouriteAddress")
      .RequireAuthorization();
  }
}
