using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Features.Addresses;

public record AddressDto (int Id,
  string FullName,
  string AddressLine1,
  string AddressLine2,
  string PostalCode,
  string City,
  string State,
  string Country,
  bool IsFavourite)
{
  public static explicit operator AddressDto(Address address) => new(address.Id,
    address.FullName,
    address.Address_line1,
    address.Address_line2,
    address.PostalCode,
    address.City,
    address.State_or_Province,
    address.Country,
    address.IsFavourite);
}