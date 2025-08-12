namespace dotnet_qrshop.Common.Models
{
  public record BaseAddressRequest(
    string FullName,
    string PhoneNumber,
    string AddressLine1,
    string AddressLine2,
    string PostalCode,
    string City,
    string State,
    string Country);
}
