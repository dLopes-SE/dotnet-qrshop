namespace dotnet_qrshop.Features.Carts.Hashing;

public record CartVersionPayload(int CartId, IEnumerable<CartItemDto> Items);