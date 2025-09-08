namespace dotnet_qrshop.Features.Common
{
  public record BaseAddress(
    string FullName,
    string PhoneNumber,
    string AddressLine1,
    string AddressLine2,
    string PostalCode,
    string City,
    string State,
    string Country);
}
