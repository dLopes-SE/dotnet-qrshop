using FluentValidation;

namespace dotnet_qrshop.Features.Addresses.Commands.Add;

public class AddAddressValidator : AbstractValidator<AddAddressCommand>
{
  public AddAddressValidator()
  {
    RuleFor(a => a.Request)
      .SetValidator(new AddressValidator());
  }
}
