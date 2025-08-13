using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Common.Results;
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
      var pendingOrder = await _dbContext.Orders
        .AsNoTracking()
        .FirstOrDefaultAsync(
          o => o.UserId == _userContext.UserId &&
            (
              o.Status == OrderStatusEnum.Pending
              || o.Status == OrderStatusEnum.Paying
              || o.Status == OrderStatusEnum.PaymentFailed
            ), 
          cancellationToken
        );

      return pendingOrder is not null;
    }

    public Result<Task<bool>> IsCartChangeAllowed()
    {
      throw new NotImplementedException();
    }
  }
}
