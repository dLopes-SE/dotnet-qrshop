using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Items.Commands.Delete;

public class DeleteItemCommandHandler(
  ApplicationDbContext _dbContext) : ICommandHandler<DeleteItemCommand>
{
  public async Task<Result> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
  {
    var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == command.Id, cancellationToken);
    if (item is null)
    {
      return Result.Failure(Error.NotFound("Item not found.", "Found no item with the requested Id."));
    }

    _dbContext.Items.Remove(item);
    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Problem("Error deleting item", "Error deleting item, please try again or contact the support"));
    }

    return Result.Success();
  }
}
