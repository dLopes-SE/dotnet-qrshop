using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public sealed record GetCartQuery : IQuery<IEnumerable<CartItemDto>>;
