using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public record CartDto(int Quantity, double SubTotal, IEnumerable<CartItemDto> Items);
