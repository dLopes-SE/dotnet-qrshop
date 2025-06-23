using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Extensions;

namespace dotnet_qrshop.Infrastructure.Authentication;

public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
{
  public Guid UserId =>
    _httpContextAccessor
      .HttpContext?
      .User
      .GetUserId()
    ?? throw new ApplicationException("User context is unavailable");
}
