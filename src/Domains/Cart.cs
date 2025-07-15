using dotnet_qrshop.Features.Carts.Hashing;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class Cart : BaseEntity
{
  public Guid UserId { get; private set; }
  public ApplicationUser User { get; private set; }

  [JsonIgnore]
  public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
  private readonly List<CartItem> _items = [];

  public string VersionHash { get; private set; } = null;

  public Cart() { }

  public Cart(ApplicationUser user) => UserId = user.Id;

  public void SetVersionHash(string hash)
  {
    VersionHash = hash;
  }
}
