using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.WeatherForecast;

internal sealed class WeatherForecastQueryHandler : IQueryHandler<WeatherForecastQuery, WeatherForecastResponse>
{
  public Task<Result<WeatherForecastResponse>> Handle(WeatherForecastQuery query, CancellationToken cancellationToken)
  {
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var response = new WeatherForecastResponse
    {
      Weather = Enumerable.Range(1, 5).Select(index =>
        new Entities.WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
    };

    return Task.FromResult(Result.Success(response));
  }
}
