using dotnet_qrshop.Domains;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
}
