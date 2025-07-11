using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public sealed record GetCartQuery : IQuery<IEnumerable<CartItemDto>>;
