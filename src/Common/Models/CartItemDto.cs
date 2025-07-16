using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Common.Models;

public record CartItemDto(int Id, int ItemId, string ItemName, string ItemImage, int Quantity)
{
  public static explicit operator CartItemDto(CartItem cartItem) => new (
    cartItem.Id,
    cartItem.ItemId,
    cartItem.Item.Name,
    cartItem.Item.Image,
    cartItem.Quantity
  );
}