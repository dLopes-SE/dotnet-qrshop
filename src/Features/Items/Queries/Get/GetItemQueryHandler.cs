using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Features.Carts;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Items.Queries.Get;

public class GetItemQueryHandler(
  ApplicationDbContext _dbContext,
  IHttpContextAccessor _contextAccessor,
  IUserContext _userContext): IQueryHandler<GetItemQuery, GetItemDto>
{
  public async Task<Result<GetItemDto>> Handle(GetItemQuery query, CancellationToken cancellationToken)
  {
    var item = await _dbContext.Items
      .AsNoTracking()
      .FirstOrDefaultAsync(i => i.Id == query.Id, cancellationToken);

    if (item is null)
    {
      return Result.Failure<GetItemDto>(Error.NotFound("Item not found.", "There is no item with the requested Id"));
    }

    var httpContext = _contextAccessor.HttpContext;
    if (httpContext is null)
    {
      return Result.Failure<GetItemDto>(Error.Failure("Failed to obtain HttpContext", "Error adding item, please try again or contact the support"));
    }

    var cartItem = new CartItemDetails(null, 0);
    if (httpContext.User?.Identity?.IsAuthenticated is true)
    {
      cartItem = await _dbContext.CartItems
        .AsNoTracking()
        .Where(ci => ci.Cart.UserId == _userContext.UserId && ci.ItemId == item.Id)
        .Select(ci => new CartItemDetails(ci.Id, ci.Quantity))
        .FirstOrDefaultAsync(cancellationToken)
        ?? new CartItemDetails(null, 0);
    }

    return Result.Success(GetItemDto.Parse((ItemDto)item, cartItem.CartItemId, cartItem.Quantity));
  }
}