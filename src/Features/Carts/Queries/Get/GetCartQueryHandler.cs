using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public class GetCartQueryHandler(
  IUserContext _userContext,
  ApplicationDbContext _dbContext) : IQueryHandler<GetCartQuery, CartDto>
{
  private const int CART_PREVIEW_ITEMS_NO = 3;
  public async Task<Result<CartDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart
      .AsNoTracking()
      .Where(c => c.UserId == _userContext.UserId)
      .Include(c => c.Items)
        .ThenInclude(ci => ci.Item)
      .Select(c => new CartDto
        (
          c.Items.Sum(ci => ci.Quantity),
          c.Items.Sum(ci => ci.Quantity * ci.Item.Price),
          (query.IsCartPreview
          ? c.Items
              .OrderByDescending(ci => ci.Id)
              .Take(CART_PREVIEW_ITEMS_NO)
              .Select(ci => (CartItemDto)ci)
          : c.Items.Select(ci => (CartItemDto)ci))
        )
      )
      .FirstOrDefaultAsync(cancellationToken);

    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure<CartDto>(Error.Failure("Internal error", "Error getting cart, please try again or contact the support"));
    }

    return cart;
  }
}
