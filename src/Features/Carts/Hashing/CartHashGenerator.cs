using System.Security.Cryptography;
using System.Text;

namespace dotnet_qrshop.Features.Carts.Hashing;

public static class CartHashGenerator
{
  public static string Generate(CartVersionPayload payload)
  {
    var serialized = CartHashSerializer.SerializeDeterministically(payload);
    var bytes = Encoding.UTF8.GetBytes(serialized);
    var hash = SHA256.HashData(bytes);

    return Convert.ToBase64String(hash);
  }
}
