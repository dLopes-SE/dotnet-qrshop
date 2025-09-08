using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem;

public class AddCartItemCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IOrderService _orderService) : ICommandHandler<AddCartItemCommand, int>
{
  public async Task<Result<int>> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);
    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure<int>(Error.Failure("Cart is null", "Error adding item to cart, please try again or contact the support"));
    }

    if (!await _orderService.IsCartChangeAllowedAsync(cancellationToken))
    {
      return Result.Failure<int>(Error.Problem("There's a pending checkout", "Error adding item to cart, please try again or contact the support"));
    }

    // Add to cart
    var cartItem = (CartItem)command.request;
    cart.AddItem(cartItem);

    // Add to order (if exists)
    (await _orderService.GetPendingOrderWithItems(cancellationToken))?.AddItem(cartItem);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure<int>(Error.Failure("Error inserting item", "Error adding item to cart, please try again or contact the support"));
    }

    return Result.Success(cartItem.Id);
  }
}
