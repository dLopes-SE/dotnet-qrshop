using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Carts;

public record CartItemDetailsDto(
  int Id,
  int ItemId,
  string Name,
  string Image,
  int Quantity,
  string Slogan,
  string Description
) : CartItemDto(Id, ItemId, Name, Image, Quantity)
{
  public static explicit operator CartItemDetailsDto(CartItem cartItem) => new(
    cartItem.Id,
    cartItem.ItemId,
    cartItem.Item.Name,
    cartItem.Item.Image,
    cartItem.Quantity,
    cartItem.Item.Slogan,
    cartItem.Item.Description
  );
}