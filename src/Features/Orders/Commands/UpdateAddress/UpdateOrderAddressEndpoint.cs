using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

public class UpdateOrderAddressEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPatch("/order/checkout/address", async (
      UpdateOrderAddressRequest request,
      ICommandHandler<UpdateOrderAddressCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle((UpdateOrderAddressCommand)request, cancellationToken);

      result.Match(Results.NoContent, CustomResults.Problem);
    });
  }
}
