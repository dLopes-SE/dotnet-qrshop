using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem;

public sealed record AddCartItemCommand(CartItemRequest request) : ICommand<int>
{
  public static explicit operator AddCartItemCommand(CartItemRequest r) => new(r);
};
