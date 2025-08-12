using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Addresses.Commands;

public record AddressRequest (
  string FullName,
  string PhoneNumber,
  string AddressLine1,
  string AddressLine2,
  string PostalCode,
  string City,
  string State,
  string Country,
  bool IsFavourite) : 
  BaseAddressRequest(FullName,
  PhoneNumber,
  AddressLine1,
  AddressLine2,
  PostalCode,
  City,
  State,
  Country);
