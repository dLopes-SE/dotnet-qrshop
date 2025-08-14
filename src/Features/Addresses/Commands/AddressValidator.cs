using dotnet_qrshop.Features.Common;
using FluentValidation;

namespace dotnet_qrshop.Features.Addresses.Commands;

public sealed class AddressValidator : AbstractValidator<AddAddressRequest>
{
  public AddressValidator()
  {
    RuleFor(a => a.Address)
      .SetValidator(new BaseAddressValidator());
  }
  
}
