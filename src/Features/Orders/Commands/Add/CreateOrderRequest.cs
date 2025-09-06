using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public record CreateOrderRequest(int? AddressId, BaseAddressRequest AddressRequest)
{
  public static CreateOrderRequest Parse(int? addressId, BaseAddressRequest request) => new(addressId, request);
}
