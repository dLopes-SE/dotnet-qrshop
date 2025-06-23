using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.WeatherForecast;

public sealed record WeatherForecastQuery : IQuery<WeatherForecastResponse>;
