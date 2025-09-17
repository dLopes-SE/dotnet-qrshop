using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Services;

public class OrderService(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : IOrderService
{
  public async Task<bool> IsCartChangeAllowedAsync(CancellationToken cancellationToken)
  {
    return await HasBlockingOrder(cancellationToken);
  }

  public async Task<bool> IsAddressChangeAllowedAsync(CancellationToken cancellationToken)
  {
    return await HasBlockingOrder(cancellationToken);
  }

  public async Task<bool> HasPendingCheckout(CancellationToken cancellationToken)
  {
    var order = await _dbContext.Orders
      .AsNoTracking()
      .AnyAsync(
        o => o.UserId == _userContext.UserId &&
          (
            o.Status == OrderStatusEnum.Pending
            || o.Status == OrderStatusEnum.Paying
            || o.Status == OrderStatusEnum.PaymentFailed
          ),
        cancellationToken
      );

    return order;
  }

  public async Task<Order> GetPendingOrderWithItems(CancellationToken cancellationToken)
  {
    return await _dbContext.Orders
      .Include(o => o.Items)
      .FirstOrDefaultAsync(o => o.UserId == _userContext.UserId && o.Status == OrderStatusEnum.Pending, cancellationToken);
  }

  public async Task<Order> GetPendingOrder(CancellationToken cancellationToken)
  {
    return await _dbContext.Orders
      .AsNoTracking()
      .FirstOrDefaultAsync
      (
        o => o.UserId == _userContext.UserId
        && o.Status == OrderStatusEnum.Pending,
        cancellationToken
      );
  }
  public async Task<Order> GetPendingOrderForUpdate(CancellationToken cancellationToken)
  {
    return await _dbContext.Orders
      .FirstOrDefaultAsync
      (
        o => o.UserId == _userContext.UserId
        && o.Status == OrderStatusEnum.Pending,
        cancellationToken
      );
  }

  public async Task<OrderStatusEnum> GetCheckoutStatus(CancellationToken cancellationToken)
  {
    var status = await _dbContext.Orders
    .AsNoTracking()
    .Where(c =>
        c.User.Id == _userContext.UserId &&
        new[] { OrderStatusEnum.Pending, OrderStatusEnum.Paying, OrderStatusEnum.PaymentFailed }.Contains(c.Status))
    .Select(c => (OrderStatusEnum?)c.Status)
    .FirstOrDefaultAsync(cancellationToken);

    return status ?? OrderStatusEnum.None;
  }

  #region private methods
  private async Task<bool> HasBlockingOrder(CancellationToken cancellationToken)
  {
    var hasBlockingOrder = await _dbContext.Orders
      .AsNoTracking()
      .AnyAsync(
        o => o.UserId == _userContext.UserId &&
          (o.Status == OrderStatusEnum.Paying
            || o.Status == OrderStatusEnum.PaymentFailed
          ),
        cancellationToken
      );

    return !hasBlockingOrder;
  }
  #endregion
}