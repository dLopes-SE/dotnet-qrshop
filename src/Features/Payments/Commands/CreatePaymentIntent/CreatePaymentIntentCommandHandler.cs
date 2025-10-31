using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RedLockNet;

namespace dotnet_qrshop.Features.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IPaymentService _paymentService,
  IDistributedLockFactory _lockFactory,
  IOptions<RedlockSettings> redlockOptions) : ICommandHandler<CreatePaymentIntentCommand, string>
{
  private readonly RedlockSettings _lockSettings = redlockOptions.Value;
  public async Task<Result<string>> Handle(CreatePaymentIntentCommand command, CancellationToken cancellationToken)
  {
    using var redlock = await _lockFactory.CreateLockAsync("test", _lockSettings.Expiry, _lockSettings.Wait, _lockSettings.Retry);
    if (!redlock.IsAcquired)
    {
      // TODO DYLAN: LOG here
      return Result.Failure<string>(Error.Conflict("Couldn't create payment intent", "Error processing payment, please try again or contact the support"));
    }

    var orderInfo = await _dbContext.Orders
      .Where(o => o.UserId == _userContext.UserId && o.Id == command.OrderId)
      .Select(o => new OrderInfo
      (
        o, 
        o.Items.Sum(oi => oi.Quantity * oi.Item.Price)
      )).FirstOrDefaultAsync(cancellationToken);

    if (orderInfo?.Order is null || orderInfo.Order.Status is not OrderStatusEnum.CheckoutPending)
    {
      return Result.Failure<string>(Error.Problem("No pending checkout", "Error processing payment, please try again or contact the support"));
    }

    if (!string.IsNullOrEmpty(orderInfo.Order.PaymentIntentId))
    {
      var clientSecretResult = await _paymentService.GetExistingPaymentIntent(orderInfo.Order.PaymentIntentId, cancellationToken);
      if (clientSecretResult.IsFailure)
      {
        return clientSecretResult;
      }

      return Result.Success(clientSecretResult.Value);
    }

    if (orderInfo.TotalPrice == 0)
    {
      return Result.Failure<string>(Error.Problem("No items in the checkout", "Error processing payment, please try again or contact the support"));
    }

    var createPaymentIntentResult = await _paymentService.CreatePaymentIntent(command.OrderId, (decimal) orderInfo.TotalPrice, cancellationToken);
    if (createPaymentIntentResult.IsFailure)
    {
      return Result.Failure<string>(createPaymentIntentResult.Error);
    }

    var (paymentIntentId, clientSecret) = createPaymentIntentResult.Value;

    orderInfo.Order.SetPaymentIntent(paymentIntentId);
    var result = await _dbContext.SaveChangesAsync(cancellationToken);

    if (result <= 0)
    {
      return Result.Failure<string>(Error.Failure("Error retriving existing paymentIntent", "Error processing payment, please try again or contact the support"));
    }

    return Result.Success(clientSecret);
  }

  private record OrderInfo(Order Order, double TotalPrice);
}
