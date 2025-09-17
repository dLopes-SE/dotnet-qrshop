using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Features.Common;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

public class UpdateOrderAddressCommandHandler
  (ApplicationDbContext _dbContext,
    IOrderService _orderService
  ): ICommandHandler<UpdateOrderAddressCommand>
{
  public async Task<Result> Handle(UpdateOrderAddressCommand command, CancellationToken cancellationToken)
  {
    if (command.Request.AddressId is not null && command.Request.AddressRequest is not null)
    {
      return Result.Failure(Error.Problem("Only one of the two properties must be present: addressId; addressRequest", "Error updating order's address, please try again or contact the support"));
    }

    if (NoAddressProvided(command.Request))
    {
      return Result.Failure(Error.Problem("No address provided", "Error updating order's address, please try again or contact the support"));
    }

    var order = await _orderService.GetPendingOrderForUpdate(cancellationToken);
    if (order is null)
    {
      return Result.Failure(Error.Problem("No pending checkout", "Error updating order's address, please try again or contact the support"));
    }

    if (command.Request.AddressId is not null && command.Request.AddressId > 0)
    {
      var address = await _dbContext.Addresses
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Id == command.Request.AddressId);

      if (address is null)
      {
        return Result.Failure(Error.Problem("Address not found", "Error updating order's address, please try again or contact the support"));
      }

      order.AddressId = command.Request.AddressId;
      order.UpdateAddress((BaseAddress)address);
    }

    if (command.Request.AddressRequest is not null)
    {
      order.AddressId = null;
      order.UpdateAddress(command.Request.AddressRequest);
    }

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Failure("Error updating order's address", "Error updating order's address, please try again or contact the support"));
    }

    return Result.Success();
  }

  private static bool NoAddressProvided(UpdateOrderAddressRequest request) => (request.AddressId is null || request.AddressId == 0) && request.AddressRequest is null;
}
