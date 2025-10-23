using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Payments.Webhook;

public class PaymentWebhookEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("webhook/payment", async (
      HttpRequest request,
      ICommandHandler<PaymentWebhookCommand> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new PaymentWebhookCommand(request), cancellationToken);
      return result.Match(() => Results.Ok(), CustomResults.Problem);
    })
      .WithName("PaymentWebhook");
  }
}
