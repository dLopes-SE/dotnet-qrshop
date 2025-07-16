namespace dotnet_qrshop.Common.Extensions;

public static class CookiesExtensions
{

  public static void SetCookie(this HttpResponse response, string key, string value, TimeSpan? lifetime = null)
  {
    response?.Cookies.Append(key, value, new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        MaxAge = lifetime ?? TimeSpan.FromDays(7),
        Domain = "localhost",
        Path = "/"
      }
    );
  }

  public static string? GetCookie(this HttpRequest request, string key)
  {
    return request.Cookies.TryGetValue(key, out var value) ? value : null;
  }

  public static void DeleteCookie(this HttpResponse response, string key)
  {
    response?.Cookies.Append(key, "", new CookieOptions
    {
      Expires = DateTimeOffset.UtcNow.AddDays(-1), // Expire in the past
      MaxAge = TimeSpan.Zero,
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.None,
      Path = "/"
    });
  }
}
