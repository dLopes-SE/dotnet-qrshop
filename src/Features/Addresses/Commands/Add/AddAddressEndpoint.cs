using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Addresses.Commands.Add;

public class AddAddressEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("user/address", async (
      [FromBody] AddAddressRequest request,
      ICommandHandler<AddAddressCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new AddAddressCommand(request), cancellationToken);

      return result.Match(Results.Created, CustomResults.Problem);
    })
      .WithName("AddAddress")
      .RequireAuthorization();
  }
}
