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
}
