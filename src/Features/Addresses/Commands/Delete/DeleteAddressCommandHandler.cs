using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Addresses.Commands.Delete;

public class DeleteAddressCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<DeleteAddressCommand>
{
  public async Task<Result> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
  {
    var user = await _dbContext.Users
      .Include(u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId, cancellationToken);

    if (user is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.Failure("User is null", "Error removing address, please try again or contact the support"));
    }

    var address = user.Addresses.FirstOrDefault(a => a.Id == command.Id);
    if (address is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.NotFound("Address not found", "Error removing address, please try again or contact the support"));
    }

    user.RemoveAddress(address);
    
    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Failure("Error removing address", "Error removing address, please try again or contact the support"));
    }

    return Result.Success();
  }
}
