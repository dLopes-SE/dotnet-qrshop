using dotnet_qrshop.Features.Carts;

namespace dotnet_qrshop.Domains;

public class CartItem : BaseEntity
{
  public int CartId { get; private set; }
  public Cart Cart { get; private set; }

  public int ItemId { get; private set; }
  public Item Item { get; private set; }

  public int Quantity { get; private set; }

  private CartItem()
  {
    
  }
  public CartItem(int itemId, int quantity)
  {
    ItemId = itemId;
    Quantity = quantity;
  }

  public static explicit operator CartItem(CartItemRequest r) => new(r.itemId, r.Quantity);

  public void UpdateQuantity(int quantity) => Quantity = quantity;
}
