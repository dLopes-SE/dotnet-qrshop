using dotnet_qrshop.Common.Messaging;

namespace dotnet_qrshop.Features.WeatherForecast;

public sealed record WeatherForecastQuery : IQuery<WeatherForecastResponse>;
