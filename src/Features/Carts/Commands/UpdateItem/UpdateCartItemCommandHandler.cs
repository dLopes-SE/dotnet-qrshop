﻿using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Commands.UpdateItem;

public class UpdateCartItemCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<UpdateCartItemCommand>
{
  public async Task<Result> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart
      .Include(c => c.Items)
      .FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);

    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure(Error.Failure("Cart is null", "Error updating item to cart, please try again or contact the support"));
    }

    cart.UpdateItem(command.CartItem.Id ?? 0, command.CartItem.Quantity);
    cart.UpdateHashVersion();

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure<int>(Error.Failure("Error updating item cart", "Error updating item to cart, please try again or contact the support"));
    }

    return Result.Success();
  }
}

