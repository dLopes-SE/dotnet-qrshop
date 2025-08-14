using dotnet_qrshop.Features.Addresses.Commands;
using dotnet_qrshop.Features.Addresses.Commands.Update;
using dotnet_qrshop.Features.Common;
using dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

namespace dotnet_qrshop.Domains
{
  public class Address : BaseEntity
  {
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; }

    public string FullName { get; private set; }
    public string Phone { get; private set; }
    public string Address_line1 { get; private set; }
    public string Address_line2 { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public string State_or_Province { get; private set; }
    public string Country { get; private set; }
    public bool IsFavourite { get; set; }

    private Address() { }

    public Address(
      string fullName,
      string phone,
      string addressLine1,
      string addressLine2,
      string postalCode,
      string city,
      string state,
      string country,
      bool isFavourite)
    {
      FullName = fullName;
      Phone = phone;
      Address_line1 = addressLine1;
      Address_line2 = addressLine2;
      PostalCode = postalCode;
      City = city;
      State_or_Province = state;
      Country = country;
      IsFavourite = isFavourite;
    }

    public static Address Parse(BaseAddressRequest request, bool isFavourite) =>
      new
      (
        request.FullName,
        request.PhoneNumber,
        request.AddressLine1,
        request.AddressLine2,
        request.PostalCode,
        request.City,
        request.State,
        request.Country,
        isFavourite
      );

    public void UpdateAddress(UpdateAddressRequest request)
    {
      FullName = request.Address.FullName;
      Phone = request.Address.PhoneNumber;
      Address_line1 = request.Address.AddressLine1;
      Address_line2 = request.Address.AddressLine2;
      PostalCode = request.Address.PostalCode;
      City = request.Address.City;
      State_or_Province = request.Address.State;
      Country = request.Address.Country;
    }

    public void SetFavourite(bool isFavourite) => IsFavourite = isFavourite;
  }
}
