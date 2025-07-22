using dotnet_qrshop.Infrastructure.Database.DbContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem;

internal sealed class AddCartItemValidator : AbstractValidator<CartItemRequest>
{
  private readonly ApplicationDbContext _dbContext;

  public AddCartItemValidator(ApplicationDbContext dbcontext)
  {
    _dbContext = dbcontext;

    RuleFor(i => i.itemId)
      .NotNull()
      .MustAsync(ItemExists)
      .WithMessage("Error adding item to cart, please try again or contact the support.");
  }

  private async Task<bool> ItemExists(int itemId, CancellationToken cancellationToken)
  {
    return await _dbContext.Items
      .AsNoTracking()
      .AnyAsync(i => i.Id == itemId, cancellationToken: cancellationToken);
  }
}
