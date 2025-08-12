using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;

namespace dotnet_qrshop.Features.Items.Commands.Add;

public class AddItemCommandHandler(
  ApplicationDbContext _dbContext) : ICommandHandler<AddItemCommand, ItemDto>
{
  public async Task<Result<ItemDto>> Handle(AddItemCommand command, CancellationToken cancellationToken)
  {
    var item = (Item)command.Request;
    
    await _dbContext.AddAsync(item, cancellationToken);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure<ItemDto>(Error.Failure("Error creating item", "Error adding item, please try again or contact the support"));
    }

    return Result.Success((ItemDto)item);
  }
}
