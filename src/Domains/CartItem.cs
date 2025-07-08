namespace dotnet_qrshop.Domains;

public class CartItem : BaseEntity
{
  public int CartId { get; private set; }
  public Cart Cart { get; private set; }

  public int ItemId { get; private set; }
  public Item Item { get; private set; }

  public int Quantity { get; private set; }
}
