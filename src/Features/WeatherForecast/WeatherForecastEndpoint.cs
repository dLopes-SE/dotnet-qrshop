using Carter;
using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.WeatherForecast;

public class WeatherForecastEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/weatherforecast", async (
      IQueryHandler<WeatherForecastQuery, WeatherForecastResponse> handler,
      CancellationToken cancellationToken) =>
    {
      var query = new WeatherForecastQuery();

      var result = await handler.Handle(query, cancellationToken);

      return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest();
    })
    .WithName("GetWeatherForecast");
  }
}
