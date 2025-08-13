using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.RemoveItem;

public class RemoveItemCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IOrderService _orderService) : ICommandHandler<RemoveItemCommand>
{
  public async Task<Result> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart
      .Include(c => c.Items)
        .ThenInclude(ci => ci.Item)
      .FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);

    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure(Error.Failure("Cart is null", "Error removing item, please try again or contact the support"));
    }

    if (!await _orderService.IsCartChangeAllowedAsync(cancellationToken))
    {
      return Result.Failure<int>(Error.Problem("There's a pending checkout", "Error removing item, please try again or contact the support"));
    }

    if (!cart.Items.Any(i => i.Id == command.CartItemId))
    {
      return Result.Failure(Error.NotFound("CartItem not found", "Error removing item, please try again or contact the support"));
    }

    cart.RemoveItem(cart.Items.FirstOrDefault(i =>  i.Id == command.CartItemId));
    cart.UpdateHashVersion();

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure<int>(Error.Failure("Error removing item from cart", "Error removing item, please try again or contact the support"));
    }

    return Result.Success();
  }
}
