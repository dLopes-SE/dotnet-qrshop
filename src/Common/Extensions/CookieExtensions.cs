namespace dotnet_qrshop.Common.Extensions;

public static class CookieExtensions
{

  public static void SetCookie(this HttpResponse response, string key, string value, TimeSpan? lifetime = null)
  {
    response.Cookies.Append(key, value, new CookieOptions
    {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.Strict,
      MaxAge = lifetime ?? TimeSpan.FromDays(7)
    });
  }

  public static string? GetCartHash(this HttpRequest request, string key)
  {
    return request.Cookies.TryGetValue(key, out var value) ? value : null;
  }
}
