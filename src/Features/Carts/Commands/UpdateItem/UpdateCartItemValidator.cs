using dotnet_qrshop.Infrastructure.Database.DbContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.UpdateItem;

internal sealed class UpdateCartItemValidator : AbstractValidator<CartItemDetails>
{
  public UpdateCartItemValidator()
  {
    RuleFor(i => i.Quantity)
      .NotNull()
      .InclusiveBetween(1, 99)
      .WithMessage("Quantity is required. Tip: value between 1-99.");
  }
}
