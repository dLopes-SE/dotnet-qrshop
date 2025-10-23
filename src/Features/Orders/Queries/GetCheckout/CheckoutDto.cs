using dotnet_qrshop.Domains;
using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Orders.Queries.GetCheckout;

public record CheckoutDto(int Id, int? AddressId, BaseAddress Address, IEnumerable<CartItemDetailsDto> Items)
{
  public static explicit operator CheckoutDto(Order order) => 
    new
    (
      order.Id,
      order.AddressId, 
      (BaseAddress)order, 
      order.Items.Select(oi => (CartItemDetailsDto)oi)
    );
}
