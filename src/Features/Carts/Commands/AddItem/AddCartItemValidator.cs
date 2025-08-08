using dotnet_qrshop.Infrastructure.Database.DbContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem;

internal sealed class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
  public AddCartItemValidator()
  {
    RuleFor(i => i.request.itemId)
      .NotNull()
      .WithMessage("ItemId is required.");

    RuleFor(i => i.request.Quantity)
      .NotNull()
      .WithMessage("Quantity is required.");
  }
}
