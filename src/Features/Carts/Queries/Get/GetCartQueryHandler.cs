using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public class GetCartQueryHandler(
  IUserContext _userContext,
  ApplicationDbContext _dbContext,
  IOrderService _orderService) : IQueryHandler<GetCartQuery, CartDto>
{
  private const int CART_PREVIEW_ITEMS_NO = 3;
  public async Task<Result<CartDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart
      .AsNoTracking()
      .Where(c => c.UserId == _userContext.UserId)
      .Include(c => c.Items)
        .ThenInclude(ci => ci.Item)
      .Select(c => new
      {
        TotalQuantity = c.Items.Sum(ci => ci.Quantity),
        TotalPrice = c.Items.Sum(ci => ci.Quantity * ci.Item.Price),
        Items = (query.IsCartPreview
          ? c.Items
              .OrderByDescending(ci => ci.Id)
              .Take(CART_PREVIEW_ITEMS_NO)
              .Select(ci => (CartItemDto)ci)
          : c.Items.Select(ci => (CartItemDetailsDto)ci))
      })
      .FirstOrDefaultAsync(cancellationToken);

    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure<CartDto>(Error.Failure("Internal error", "Error getting cart, please try again or contact the support"));
    }

    var checkoutStatus = await _orderService.GetCheckoutStatus(cancellationToken);
    var isCartChangeAllowed = await _orderService.IsCartChangeAllowedAsync(cancellationToken);

    return new CartDto(
      cart.TotalQuantity,
      cart.TotalPrice,
      checkoutStatus is not OrderStatusEnum.Paying and not OrderStatusEnum.PaymentFailed,
      checkoutStatus.ToString(),
      cart.Items
    );
  }
}
