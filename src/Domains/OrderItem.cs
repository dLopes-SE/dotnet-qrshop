namespace dotnet_qrshop.Domains;

public class OrderItem : BaseEntity
{
  public int OrderId { get; private set; }
  public Order Order { get; private set; }
  public int ItemId { get; private set; }
  public Item Item { get; private set; }
  public int Quantity { get; private set; }

  private OrderItem() { }
  public OrderItem(int itemId,  int quantity)
  {
    ItemId = itemId;
    Quantity = quantity;    
  }

  public static explicit operator OrderItem(CartItem cartItem) => new(cartItem.ItemId, cartItem.Quantity);
}
