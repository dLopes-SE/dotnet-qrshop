using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem
{
  public class AddCartItemEndpoint : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapPost("shop/cart", async (
        CartItemRequest request,
        ICommandHandler<AddCartItemCommand, CartItemDto> handler,
        CancellationToken cancellationToken) =>
      {
        var result = await handler.Handle((AddCartItemCommand)request, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
      })
        .WithName("AddCartItem")
        .RequireAuthorization();
    }
  }
}
