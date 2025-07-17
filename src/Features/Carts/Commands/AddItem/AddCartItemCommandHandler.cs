using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.AddItem
{
  public class AddCartItemCommandHandler(
    ApplicationDbContext _dbContext,
    IUserContext _userContext) : ICommandHandler<AddCartItemCommand, CartItemDto>
  {
    public async Task<Result<CartItemDto>> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
      var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);
      if (cart is null)
      {
        // TODO DYLAN: Log error here
        return Result.Failure<CartItemDto>(Error.Failure("Cart is null", "Error getting cart, please try again or contact the support"));
      }

      // Add item
      var cartItem = (CartItem)command.request;
      cart.AddItem(cartItem);

      // Update cart version hash
      cart.UpdateHashVersion();
      
      var insertResult = await _dbContext.SaveChangesAsync(cancellationToken);
      if (insertResult <= 0)
      {
        // TODO Dylan: LOG ERROR HERE
        return Result.Failure<CartItemDto>(Error.Failure("Error inserting item", "Error getting cart, please try again or contact the support"));
      }

      return Result.Success((CartItemDto)cartItem);
    }
  }
}
