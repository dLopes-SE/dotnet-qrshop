using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Common;

public record BaseAddress(
  string FullName,
  string PhoneNumber,
  string AddressLine1,
  string? AddressLine2,
  string PostalCode,
  string City,
  string State,
  string Country)
{
  public static explicit operator BaseAddress(Order order) =>
    new
    (
      order.FullName,
      order.Phone,
      order.Address_line1,
      order.Address_line2,
      order.PostalCode,
      order.City,
      order.State_or_Province,
      order.Country
    );

  public static explicit operator BaseAddress(Address address) =>
    new
    (
      address.FullName,
      address.Phone,
      address.Address_line1,
      address.Address_line2,
      address.PostalCode,
      address.City,
      address.State_or_Province,
      address.Country
    );
}
