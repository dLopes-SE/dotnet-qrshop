using dotnet_qrshop.Common.Hashing;
using dotnet_qrshop.Common.Hashing.CartHashVersion;
using dotnet_qrshop.Common.Models;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class Cart : BaseEntity
{
  public Guid UserId { get; set; }
  public ApplicationUser User { get; set; }

  [JsonIgnore]
  public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
  private readonly List<CartItem> _items = [];

  public string VersionHash { get; set; } = string.Empty;

  public Cart() { }

  public Cart(ApplicationUser user) => UserId = user.Id;

  #region CartItems
  public void AddItem(CartItem item) => _items.Add(item);
  public void UpdateItem(int cartItemId, int quantity) => _items.FirstOrDefault(ci => ci.Id == cartItemId)?.UpdateQuantity(quantity);
  #endregion

  #region HashVersion
  public void UpdateHashVersion()
  {
    var itemDtos = _items.Select(i => new CartItemDto(i.Id, i.ItemId, i.Item.Name, i.Item.Image, i.Quantity));
    var payload = new CartVersionPayload(Id, itemDtos);
    VersionHash = CartHashGenerator.Generate(payload);
  }
  #endregion
}
