using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Items.Queries.Get;

public class GetItemQueryHandler(
  ApplicationDbContext _dbContext): IQueryHandler<GetItemQuery, ItemDto>
{
  public async Task<Result<ItemDto>> Handle(GetItemQuery query, CancellationToken cancellationToken)
  {
    var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == query.Id, cancellationToken);
    if (item is null)
    {
      return Result.Failure<ItemDto>(Error.NotFound("Item not found.", "There is no item with the requested Id"));
    }

    return Result.Success((ItemDto)item);
  }
}