namespace dotnet_qrshop.Features.Carts;

public record CartItemDetails(int? Id, int Quantity)
{
  public static CartItemDetails Parse(int id, int quantity) => new(id, quantity);
}
