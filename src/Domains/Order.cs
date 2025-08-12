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
  public string VersionHash {  get; set; } = string.Empty;

  public string FullName { get; set; }
  public string Phone { get; set; }
  public string Address_line1 { get; set; }
  public string? Address_line2 { get; set; }
  public string PostalCode { get; set; }
  public string City { get; set; }
  public string State_or_Province { get; set; }
  public string Country { get; set; }
}
