using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Items.Queries.List;

public class ListItemsQueryHandler(
  ApplicationDbContext _dbContext) : IQueryHandler<ListItemsQuery, IEnumerable<ItemDto>>
{
  public async Task<Result<IEnumerable<ItemDto>>> Handle(ListItemsQuery query, CancellationToken cancellationToken)
  {
    var itemsQuery = _dbContext.Items.AsQueryable();
    if (query.FeaturedItemsOnly)
    {
      itemsQuery = itemsQuery.Where(i => i.IsFeaturedItem);
    }

    var items = await itemsQuery.ToListAsync(cancellationToken);

    return Result.Success(items.Select(i => (ItemDto)i));
  }
}
