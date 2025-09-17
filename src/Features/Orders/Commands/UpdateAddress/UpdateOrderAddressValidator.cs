using dotnet_qrshop.Features.Common;
using FluentValidation;

namespace dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

public class UpdateOrderAddressValidator : AbstractValidator<UpdateOrderAddressCommand>
{
  public UpdateOrderAddressValidator()
  {
    RuleFor(r => r.Request.AddressRequest)
      .SetValidator(new BaseAddressValidator());
  }
}
