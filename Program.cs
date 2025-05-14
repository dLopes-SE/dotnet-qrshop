using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Features.WeatherForecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IQueryHandler<,>))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (
  IQueryHandler<WeatherForecastQuery, WeatherForecastResponse> handler,
  CancellationToken cancellationToken) =>
{
  var query = new WeatherForecastQuery();

  var result = await handler.Handle(query, cancellationToken);

  return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest();
})
.WithName("GetWeatherForecast");

app.Run();