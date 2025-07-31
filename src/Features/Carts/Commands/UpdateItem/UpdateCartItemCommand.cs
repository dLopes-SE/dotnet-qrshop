using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Carts.Commands.UpdateItem;

public sealed record UpdateCartItemCommand(CartItemDetails CartItem) : ICommand
{
  public static UpdateCartItemCommand Parse(int id, int quantity) => new(CartItemDetails.Parse(id, quantity));
};
