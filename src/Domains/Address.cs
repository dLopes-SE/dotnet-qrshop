using dotnet_qrshop.Features.Addresses.Commands;

namespace dotnet_qrshop.Domains
{
  public class Address : BaseEntity
  {
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; }

    public string FullName { get; private set; }
    public string Phone {  get; private set; }
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

    public static explicit operator Address(AddressRequest request) =>
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
        request.IsFavourite
      );
  }
}
