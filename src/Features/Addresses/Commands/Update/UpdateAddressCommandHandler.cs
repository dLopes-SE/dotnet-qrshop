using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public class UpdateAddressCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IOrderService _orderService) : ICommandHandler<UpdateAddressCommand>
{
  public async Task<Result> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
  {
    var user = await _dbContext.Users
      .Include(u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId, cancellationToken);

    if (user is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.Failure("User not found", "Error updating address, please try again or contact the support"));
    }

    if (!await _orderService.IsAddressChangeAllowedAsync(cancellationToken))
    {
      return Result.Failure<int>(Error.Problem("There's a pending checkout", "Error updating address, please try again or contact the support"));
    }

    if (user.Addresses.FirstOrDefault(a => a.Id == command.Id) is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.NotFound("Address not found", "Error updating address, please try again or contact the support"));
    }

    // Update address
    user.UpdateAddress(command.Id, command.Request);

    // Update order's address (if exists)
    (await _orderService.GetPendingOrder(cancellationToken))?
      .UpdateAddress(command.Request.Address);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <=  0)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.Problem("Error updating address", "Error removing address, please try again or contact the support"));
    }

    return Result.Success();
  }
}
