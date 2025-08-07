using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
    }

    _addresses.Add(address);
  }
}
