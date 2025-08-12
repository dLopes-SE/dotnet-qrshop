using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Addresses.Commands.SetFavourite;

public class SetFavouriteAddressCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<SetFavouriteAddressCommand>
{
  public async Task<Result> Handle(SetFavouriteAddressCommand command, CancellationToken cancellationToken)
  {
    var user = await _dbContext.Users
      .Include(u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId, cancellationToken);

    if (user is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.Failure("User not found", "Error updating address, please try again or contact the support"));
    }

    if (user.Addresses.FirstOrDefault(a => a.Id == command.Id) is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.NotFound("Address not found", "Error setting favourite address, please try again or contact the support"));
    }

    user.SetFavouriteAddress(command.Id);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.Failure("Error setting favourite address", "Error setting favourite address, please try again or contact the support"));
    }

    return Result.Success();
  }
}
