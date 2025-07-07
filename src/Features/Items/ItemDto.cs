using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Items;

public sealed record ItemDto(int Id, string Name, string Slogan, string Description, ShopItemCategoryEnum Category, string Image,  bool IsFeaturedItem, double Price)
{
  public static explicit operator ItemDto(Item item) => 
    new(item.Id, item.Name, item.Slogan, item.Description, item.Category, item.Image, item.IsFeaturedItem, item.Price);
}
