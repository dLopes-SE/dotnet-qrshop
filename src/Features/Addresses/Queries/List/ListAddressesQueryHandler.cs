using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Addresses.Queries.List;

public class ListAddressesQueryHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext): IQueryHandler<ListAddressesQuery, IEnumerable<AddressDto>>
{
  public async Task<Result<IEnumerable<AddressDto>>> Handle(ListAddressesQuery query, CancellationToken cancellationToken)
  {
    var addresses = await _dbContext.Addresses
      .AsNoTracking()
      .Where(a => a.UserId == _userContext.UserId)
      .OrderByDescending(a => a.IsFavourite)
      .Select(a => (AddressDto)a)
      .ToListAsync(cancellationToken);

    return Result.Success<IEnumerable<AddressDto>>(addresses);
  }
}
