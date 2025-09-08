using dotnet_qrshop.Features.Common;
using FluentValidation;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderValidator()
  {
    RuleFor(c => c.Request.AddressRequest)
      .SetValidator(new BaseAddressValidator());
  }
}
