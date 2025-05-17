namespace dotnet_qrshop.Features.WeatherForecast;

public sealed class WeatherForecastResponse
{
  public IEnumerable<Entities.WeatherForecast> Weather { get; set; }
}
