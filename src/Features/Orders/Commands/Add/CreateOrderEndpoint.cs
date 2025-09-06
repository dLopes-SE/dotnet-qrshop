using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public class CreateOrderEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("order/create", async (
      [FromBody] CreateOrderRequest request, 
      ICommandHandler<CreateOrderCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new CreateOrderCommand(request), cancellationToken);

      return result.Match(Results.NoContent, CustomResults.Problem);

    })
      .WithName("CreateOrder")
      .RequireAuthorization();
  }
}
