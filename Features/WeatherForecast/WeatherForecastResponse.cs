namespace dotnet_qrshop.Features.WeatherForecast;

public sealed class WeatherForecastResponse
{
  public IEnumerable<Domain.WeatherForecast> Weather { get; set; }
}
