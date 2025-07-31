using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Carts.Commands.UpdateItem;

public class UpdateCartItemEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPatch("shop/cart/{id}", async (
      int id,
      [FromBody] int quantity,
      ICommandHandler<UpdateCartItemCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(UpdateCartItemCommand.Parse(id, quantity), cancellationToken);
      result.Match(Results.NoContent, CustomResults.Problem);
    });
  }
}
