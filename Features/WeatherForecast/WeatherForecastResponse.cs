namespace dotnet_qrshop.Features.WeatherForecast;

public sealed class WeatherForecastResponse
{
  public IEnumerable<Domains.WeatherForecast> Weather { get; set; }
}
