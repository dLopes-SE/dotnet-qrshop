using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Features.UserInfo.Queries.IsAdmin;

public class IsAdminQueryHandler(
  ApplicationDbContext _dbContext,
  UserManager<ApplicationUser> _userManager,
  IUserContext _userContext) : IQueryHandler<IsAdminQuery, bool>
{
  public async Task<Result<bool>> Handle(IsAdminQuery query, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
    if (user is null)
    {
      return Result.Failure<bool>(Error.Failure("User not found", "Error verifying if user's admin, please try again or contact the support"));
    }

    var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
    return Result.Success(isAdmin);
  }
}
