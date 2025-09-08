using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public record CreateOrderRequest(int? AddressId, BaseAddress AddressRequest)
{
  public static CreateOrderRequest Parse(int? addressId, BaseAddress request) => new(addressId, request);
}
