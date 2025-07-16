using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Carts.Hashing;

public record CartVersionPayload(int CartId, IEnumerable<CartItem> Items);