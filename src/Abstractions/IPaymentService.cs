using dotnet_qrshop.Common.Results;
using Stripe;

namespace dotnet_qrshop.Abstractions;
public interface IPaymentService
{
  Task<Result<(string, string)>> CreatePaymentIntent(int orderId, decimal ammount, CancellationToken cancellationToken);
  Task<Result<string>> GetExistingPaymentIntent(string stripePaymentIntentId, CancellationToken cancellationToken);
  Task HandleWebhookAsync(HttpRequest request);
}