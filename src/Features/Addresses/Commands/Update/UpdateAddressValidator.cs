using FluentValidation;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
{
  public UpdateAddressValidator()
  {
    RuleFor(a => a.request)
      .SetValidator(new AddressValidator());
  }
}
