using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using dotnet_qrshop.Infrastructure.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public class CreateOrderCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : ICommandHandler<CreateOrderCommand>
{
  public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    if (command.Request.AddressId is not null && command.Request.AddressRequest is not null)
    {
      return Result.Failure(Error.Problem("Only one of the two properties must be present: addressId; addressRequest", "Error creating order, please try again or contact the support"));
    }

    var user = await _dbContext.Users
      .Include(u => u.Cart)
        .ThenInclude(c => c.Items)
      .IncludeIf(command.Request.AddressId > 0, u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId, cancellationToken: cancellationToken);
    

    if (user is null)
    {
      return Result.Failure(Error.NotFound("User not found", "Error creating order, please try again or contact the support"));
    }

    Address? address;
    if (command.Request.AddressId > 0)
    {
      address = user.Addresses.FirstOrDefault(a => a.Id == command.Request.AddressId);
      if (address is null)
      {
        return Result.Failure(Error.NotFound("Address not found", "Error creating order, please try again or contact the support"));
      }

      return await AddOrder(user.Cart, address, cancellationToken);
    }

    if (command.Request.AddressRequest is null)
    {
      return Result.Failure(Error.Problem("Address not provided", "Error creating order, please try again or contact the support"));
    }

    throw new NotImplementedException();
  }

  private async Task<Result> AddOrder(Cart cart, Address address, CancellationToken cancellationToken)
  {
    var order = Order.Create(cart, address);
    await _dbContext.Orders.AddAsync(order, cancellationToken);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Failure("Error inserting order", "Error creating order, please try again or contact the support"));
    }

    return Result.Success();
  }
}
