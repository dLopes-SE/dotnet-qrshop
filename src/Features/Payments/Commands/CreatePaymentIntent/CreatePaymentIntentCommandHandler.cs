using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using dotnet_qrshop.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IPaymentService _paymentService) : ICommandHandler<CreatePaymentIntentCommand, string>
{
  public async Task<Result<string>> Handle(CreatePaymentIntentCommand command, CancellationToken cancellationToken)
  {
    var orderInfo = await _dbContext.Orders
      .AsNoTracking()
      .Where(o => o.UserId == _userContext.UserId && o.Status == OrderStatusEnum.Pending)
      .Select(o => new {
        o.Id,
        TotalPrice = o.Items.Sum(oi => oi.Quantity * oi.Item.Price)
      })
      .FirstOrDefaultAsync(cancellationToken);

    if (orderInfo is null)
    {
      return Result.Failure<string>(Error.Problem("No pending checkout", "Error processing payment, please try again or contact the support"));
    }

    if (orderInfo.TotalPrice == 0)
    {
      return Result.Failure<string>(Error.Problem("No items in the checkout", "Error processing payment, please try again or contact the support"));
    }

    return await _paymentService.CreatePaymentIntentAsync(orderInfo.Id, (decimal) orderInfo.TotalPrice, cancellationToken);
  }
}
