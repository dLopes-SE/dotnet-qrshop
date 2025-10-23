using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("orders/{orderId}/payment-intents", async (
      int orderId,
      ICommandHandler<CreatePaymentIntentCommand, string> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new CreatePaymentIntentCommand(orderId), cancellationToken);

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("CreatePayment")
      .RequireAuthorization();
  }
}
