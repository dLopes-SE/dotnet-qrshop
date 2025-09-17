using dotnet_qrshop.Abstractions;
using Stripe;

namespace dotnet_qrshop.Services;

public class StripePaymentService(
  IConfiguration _configuration) : IPaymentService
{
  public async Task<string> CreatePaymentIntentAsync(int orderId, decimal amount, CancellationToken cancellationToken)
  {
    StripeConfiguration.ApiKey = _configuration["StripeApiKey"];

    var options = new PaymentIntentCreateOptions
    {
      Amount = (long)(amount * 100), // Stripe expects amount in cents
      Currency = "usd",
      Metadata = new Dictionary<string, string> { { "orderId", orderId.ToString() } }
    };

    var service = new PaymentIntentService();
    var paymentIntent = await service.CreateAsync(options, cancellationToken: cancellationToken);

    return paymentIntent.ClientSecret;
  }
}
