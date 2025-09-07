using dotnet_qrshop.Common.Enums;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public record CartDto
(
  int Quantity,
  double SubTotal,
  bool IsCartChangeAllowed,
  string CheckoutStatus,
  IEnumerable<object> Items
);
