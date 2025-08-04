using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Features.Identity.Register;

namespace dotnet_qrshop.Features.Carts.Commands.RemoveItem
{
  public class RemoveItemEndpoint : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapDelete("/shop/cart/{id}", async (
        int id,
        ICommandHandler<RemoveItemCommand> handler,
        CancellationToken cancellationToken) =>
      {
        var result = await handler.Handle(new RemoveItemCommand(id), cancellationToken);

        return result.Match(() => Results.Ok(), CustomResults.Problem);
      });
    }
  }
}
