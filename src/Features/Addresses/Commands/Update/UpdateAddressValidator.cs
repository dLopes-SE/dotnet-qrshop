using dotnet_qrshop.Features.Common;
using FluentValidation;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
{
  public UpdateAddressValidator()
  {
    RuleFor(a => a.Request.Address)
      .SetValidator(new BaseAddressValidator());
  }
}
