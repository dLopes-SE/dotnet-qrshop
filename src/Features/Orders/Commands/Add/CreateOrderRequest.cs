using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public record CreateOrderRequest(int CartId, int? AddressId, BaseAddressRequest AddressRequest)
{
  public static CreateOrderRequest Parse(int cartId, int? addressId, BaseAddressRequest request) => new(cartId, addressId, request);
}
