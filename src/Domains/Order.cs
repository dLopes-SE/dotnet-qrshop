using dotnet_qrshop.Common.Enums;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class Order : BaseEntity
{
  public Guid UserId { get; set; }
  public ApplicationUser User { get; set; }
  [JsonIgnore]
  public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
  private readonly List<OrderItem> _items = [];

  public OrderStatusEnum Status { get; set; }
  public string FullName { get; set; }
  public string Phone { get; set; }
  public string Address_line1 { get; set; }
  public string? Address_line2 { get; set; }
  public string PostalCode { get; set; }
  public string City { get; set; }
  public string State_or_Province { get; set; }
  public string Country { get; set; }

  public Order() { }

  public static Order Create(Cart cart, Address address) 
  {
    var order = new Order()
    {
      UserId = cart.UserId,
      Status = OrderStatusEnum.Pending,

      FullName = address.FullName,
      Phone = address.Phone,
      Address_line1 = address.Address_line1,
      Address_line2 = address.Address_line2,
      PostalCode = address.PostalCode,
      City = address.City,
      State_or_Province = address.State_or_Province,
      Country = address.Country,
    };

    order._items.AddRange(cart.Items.Select(i => (OrderItem)i));

    return order;
  }

  public void AddItem(CartItem item) => _items.Add((OrderItem)item);
  public void UpdateItem(int itemId, int quantity) => _items.FirstOrDefault(oi => oi.ItemId == itemId)?.UpdateQuantity(quantity);
  public void RemoveItem(int itemId) => _items.RemoveAll(oi => oi.ItemId == itemId);
}
