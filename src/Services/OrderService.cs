using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Services
{
  public class OrderService(
    ApplicationDbContext _dbContext,
    IUserContext _userContext) : IOrderService
  {
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

      return !order;
    }

    public async Task<bool> IsCartChangeAllowedAsync(CancellationToken cancellationToken)
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

    public async Task<Order> GetPendingOrder(CancellationToken cancellationToken)
    {
      return await _dbContext.Orders
        .AsNoTracking()
        .FirstOrDefaultAsync(o => o.UserId == _userContext.UserId && o.Status == OrderStatusEnum.Pending, cancellationToken);
    }
  }
}
