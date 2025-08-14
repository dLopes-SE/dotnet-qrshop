using dotnet_qrshop.Features.Common;
using FluentValidation;

namespace dotnet_qrshop.Features.Addresses.Commands.Add;

public class AddAddressValidator : AbstractValidator<AddAddressCommand>
{
  public AddAddressValidator()
  {
    RuleFor(a => a.Request.Address)
      .SetValidator(new BaseAddressValidator());
  }
}
