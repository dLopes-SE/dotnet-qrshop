using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Common.Hashing.CartHashVersion;

public record CartVersionPayload(int CartId, IEnumerable<CartItemDto> Items);