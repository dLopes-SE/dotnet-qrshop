using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Features.Items;
using dotnet_qrshop.Features.UserInfo;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Addresses.Commands.Add;

public class AddAddressCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<AddAddressCommand>
{
  public async Task<Result> Handle(AddAddressCommand command, CancellationToken cancellationToken)
  {
    var user = await _dbContext.Users
      .Include(u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId, cancellationToken);

    if (user == null)
    {
      // TODO DYLAN: Log here
      return Result.Failure(Error.NotFound("User not found", "User not found"));
    }

    user.AddAddress((Address)command.Request);
    
    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Problem("Error adding address", "Error adding address, please try again or contact the support"));
    }

    return Result.Success();
  }
}
