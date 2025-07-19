namespace dotnet_qrshop.Features.Items.Queries.Get;

public record GetItemDto (ItemDto item, int quantity)
{
  public static GetItemDto Parse(ItemDto item, int quantity) => new (item, quantity);
}
