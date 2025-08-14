using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Features.Orders.Commands.UpdateAddress;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public class UpdateAddressEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPatch("user/address/{id}", async (
      int id,
      [FromBody] UpdateAddressRequest request,
      ICommandHandler<UpdateAddressCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(UpdateAddressCommand.Parse(id, request), cancellationToken);

      result.Match(Results.NoContent, CustomResults.Problem);
    })
      .WithName("UpdateAddress")
      .RequireAuthorization();
  }
}
