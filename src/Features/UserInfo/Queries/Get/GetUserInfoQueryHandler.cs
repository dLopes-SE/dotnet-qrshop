using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.UserInfo.Queries.Get;

public class GetUserInfoQueryHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext) : IQueryHandler<GetUserInfoQuery, UserInfoDto>
{
  public async Task<Result<UserInfoDto>> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
  {
    // TODO DYLAN: This will be in a repository later
    var user = await _dbContext.Users
      .AsNoTracking()
      .Include(u => u.Addresses)
      .FirstOrDefaultAsync(u => u.Id == _userContext.UserId);

    if (user is null)
    {
      // TODO DYLAN: Log here
      return Result.Failure<UserInfoDto>(Error.NotFound("User not found", "User not found"));
    }

    return Result.Success((UserInfoDto)user);
  }
}
