using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Items;

public sealed record ItemDto(int Id, string Name, string Slogan, string Description, string Image, bool IsFeaturedItem)
{
  public static explicit operator ItemDto(Item item) => 
    new(item.Id, item.Name, item.Slogan, item.Description, item.Image, item.IsFeaturedItem);
}
