using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Features.Carts.Hashing;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public class GetCartQueryHandler(
  IUserContext _userContext,
  ApplicationDbContext _dbContext) : IQueryHandler<GetCartQuery, IEnumerable<CartItemDto>>
{
  public async Task<Result<IEnumerable<CartItemDto>>> Handle(GetCartQuery query, CancellationToken cancellationToken)
  {
    var cart = await _dbContext.Cart
      .AsNoTracking()
      .Include(c => c.Items)
        .ThenInclude(ci => ci.Item)
      .FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);

    if (cart is null)
    {
      var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _userContext.UserId);
      if (user is null)
      {
        return Result.Failure<IEnumerable<CartItemDto>>(Error.Failure("Internal error", "Error getting cart, please try again or contact the support"));
      }

      cart = new(user);
      await _dbContext.AddAsync(cart, cancellationToken);
      cart.SetVersionHash("123");
      await _dbContext.SaveChangesAsync(cancellationToken);

      // Generate versionHash
      cart.SetVersionHash(CartHashGenerator.Generate(new CartVersionPayload(cart.Id, [])));
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

    if (cart is null)
    {
      // TODO DYLAN: Log error here
      return Result.Failure<IEnumerable<CartItemDto>>(Error.Failure("Internal error", "Error getting cart, please try again or contact the support"));
    }

    return Result.Success(cart.Items.Select(item => (CartItemDto)item));
  }
}
