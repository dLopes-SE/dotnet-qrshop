using dotnet_qrshop.Features.Carts.Hashing;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class Cart : BaseEntity
{
  public Guid UserId { get; set; }
  public ApplicationUser User { get; set; }

  [JsonIgnore]
  public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
  private readonly List<CartItem> _items = [];

  public string VersionHash { get; set; } = null;

  public Cart() { }

  public Cart(ApplicationUser user) => UserId = user.Id;

  public void UpdateHashVersion()
  {
    var payload = new CartVersionPayload(Id, _items);
    VersionHash = CartHashGenerator.Generate(payload);
  }
}
