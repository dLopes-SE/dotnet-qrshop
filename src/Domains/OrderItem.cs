namespace dotnet_qrshop.Domains;

public class OrderItem : BaseEntity
{
  public int OrderId { get; private set; }
  public Order Order { get; private set; }
  public int ItemId { get; private set; }
  public Item Item { get; private set; }
  public int Quantity { get; private set; }
}
