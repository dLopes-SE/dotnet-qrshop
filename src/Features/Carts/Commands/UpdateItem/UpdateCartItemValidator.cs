using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.UpdateItem;

internal sealed class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
  public UpdateCartItemValidator()
  {
    RuleFor(i => i.CartItem.Id)
      .NotEmpty()
      .GreaterThan(0)
      .WithErrorCode("Invalid itemId");

    RuleFor(i => i.CartItem.Quantity)
      .NotEmpty()
      .InclusiveBetween(1, 99)
      .WithMessage("Quantity is required. Tip: value between 1-99.");
  }
}
