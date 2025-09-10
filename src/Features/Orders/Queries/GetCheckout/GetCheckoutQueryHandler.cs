using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Orders.Queries.GetCheckout;

public class GetCheckoutQueryHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : IQueryHandler<GetCheckoutQuery, CheckoutDto>
{
  public async Task<Result<CheckoutDto>> Handle(GetCheckoutQuery query, CancellationToken cancellationToken)
  {
    var checkout = await _dbContext.Orders
      .AsNoTracking()
      .Include(o => o.Items)
        .ThenInclude(oi => oi.Item)
      .FirstOrDefaultAsync(o => o.UserId == _userContext.UserId && o.Status == OrderStatusEnum.Pending, cancellationToken);

    if (checkout is null)
    {
      return Result.Failure<CheckoutDto>(Error.NotFound("Checkout not found", "Error getting checkout, please try again or contact the support"));
    }

    return Result.Success((CheckoutDto)checkout);
  }
}
