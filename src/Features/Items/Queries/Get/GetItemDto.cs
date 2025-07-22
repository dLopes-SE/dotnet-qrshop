namespace dotnet_qrshop.Features.Items.Queries.Get;

public record GetItemDto (ItemDto item, int? cartItemId, int quantity)
{
  public static GetItemDto Parse(ItemDto item, int? cartItemId, int quantity) => new (item, cartItemId, quantity);
}
