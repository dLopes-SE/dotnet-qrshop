using dotnet_qrshop.Common.Hashing.CartHashVersion;
using dotnet_qrshop.Common.Models;

public class CartHashGeneratorTests
{
  private CartVersionPayload CreatePayload(IEnumerable<CartItemDto> items)
      => new(1, items);

  [Fact, Trait("Cart", "HashGenerator")]
  public void Generate_SamePayloads_ShouldProduceSameHash()
  {
    var items = new[]
    {
      new CartItemDto(1, 100, "Item A", "image.png", 2),
      new CartItemDto(2, 101, "Item B", "image2.png", 1)
    };

    var payload1 = CreatePayload(items);
    var payload2 = CreatePayload(items);

    var hash1 = CartHashGenerator.Generate(payload1);
    var hash2 = CartHashGenerator.Generate(payload2);

    Assert.Equal(hash1, hash2);
  }

  [Fact, Trait("Cart", "HashGenerator")]
  public void Generate_DifferentOrder_ShouldProduceSameHash()
  {
    var items1 = new[]
    {
      new CartItemDto(1, 100, "Item A", "image.png", 2),
      new CartItemDto(2, 101, "Item B", "image2.png", 1)
    };

    var items2 = new[]
    {
      new CartItemDto(2, 101, "Item B", "image2.png", 1),
      new CartItemDto(1, 100, "Item A", "image.png", 2)
    };

    var payload1 = CreatePayload(items1);
    var payload2 = CreatePayload(items2);

    var hash1 = CartHashGenerator.Generate(payload1);
    var hash2 = CartHashGenerator.Generate(payload2);

    Assert.Equal(hash1, hash2);
  }

  [Fact, Trait("Cart", "HashGenerator")]
  public void Generate_DifferentPayloads_ShouldProduceDifferentHashes()
  {
    var items1 = new[]
    {
      new CartItemDto(1, 100, "Item A", "image.png", 2),
      new CartItemDto(2, 101, "Item B", "image2.png", 1)
    };

    var items2 = new[]
    {
      new CartItemDto(1, 100, "Item A", "image.png", 3), // quantity changed
      new CartItemDto(2, 101, "Item B", "image2.png", 1)
    };

    var payload1 = CreatePayload(items1);
    var payload2 = CreatePayload(items2);

    var hash1 = CartHashGenerator.Generate(payload1);
    var hash2 = CartHashGenerator.Generate(payload2);

    Assert.NotEqual(hash1, hash2);
  }
}
