using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem;

public class AddCartItemCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<AddCartItemCommand, int>
{
  public async Task<Result<int>> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);
    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure<int>(Error.Failure("Cart is null", "Error adding item to cart, please try again or contact the support"));
    }

    using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    try
    {
      // Add item
      var cartItem = (CartItem)command.request;
      cart.AddItem(cartItem);

      var result = await _dbContext.SaveChangesAsync(cancellationToken);
      if (result <= 0)
      {
        return Result.Failure<int>(Error.Failure("Error inserting item", "Error adding item to cart, please try again or contact the support"));
      }

      // Load the item after insert
      await _dbContext.Entry(cartItem)
        .Reference(ci => ci.Item)
        .LoadAsync(cancellationToken);

      // Update cart version hash
      cart.UpdateHashVersion();

      result = await _dbContext.SaveChangesAsync(cancellationToken);
      if (result <= 0)
      {
        await transaction.RollbackAsync(cancellationToken);
        return Result.Failure<int>(Error.Failure("Error updating version", "Error adding item to cart, please try again or contact the support"));
      }

      await transaction.CommitAsync(cancellationToken);

      return Result.Success(cartItem.Id);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync(cancellationToken);
      // Log error
      return Result.Failure<int>(Error.Failure("Unexpected error occurred", "Error adding item to cart, please try again or contact the support"));
    }
  }
}
