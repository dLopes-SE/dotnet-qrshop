using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class Cart : BaseEntity
{
  public Guid UserId { get; private set; }
  public ApplicationUser User { get; private set; }

  [JsonIgnore]
  public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
  private readonly List<CartItem> _items = [];
  
  public string VersionHash { get; private set; }
}
