using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Carts.Hashing;

public record CartVersionPayload(int CartId, IEnumerable<CartItemDto> Items);