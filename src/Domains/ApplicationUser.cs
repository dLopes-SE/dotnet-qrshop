using dotnet_qrshop.Features.Addresses.Commands.Update;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Domains;

public class ApplicationUser : IdentityUser<Guid>
{
  public string Name { get; set; }

  [JsonIgnore]
  public IReadOnlyList<Address> Addresses => _addresses.AsReadOnly();
  private readonly List<Address> _addresses = [];

  public void AddAddress(Address address) {
    if (_addresses.Count == 0 && !address.IsFavourite)
    {
      address.IsFavourite = true;
      _addresses.Add(address);

      return;
    }

    if (address.IsFavourite && _addresses.Count > 0)
    {
      _addresses.ForEach(a => a.SetFavourite(false));
    }

    _addresses.Add(address);
  }

  public void RemoveAddress(Address address) => _addresses.Remove(address);
  public void UpdateAddress(int addressId, UpdateAddressRequest address) => _addresses.FirstOrDefault(a => a.Id == addressId)?.UpdateAddress(address);
  public void SetFavouriteAddress(int addressId)
  {
    if (_addresses.FirstOrDefault(a => a.Id == addressId)?.IsFavourite ?? false)
    {
      return;
    }

    _addresses.ForEach(a => a.SetFavourite(false));
    _addresses.FirstOrDefault(a => a.Id == addressId)?
      .SetFavourite(true);
  }
}
