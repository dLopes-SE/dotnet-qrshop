namespace dotnet_qrshop.Features.Carts.Queries.Get;

public record CartDto(int Quantity, double SubTotal, bool IsCartChangeAllowed, IEnumerable<object> Items);
