using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Features.Payments.Webhook;
using Stripe;

namespace dotnet_qrshop.Services;

public class StripePaymentService : IPaymentService
{
  private readonly string _apiSecretKey;
  private readonly string _webhookSecret;

  public StripePaymentService(IConfiguration configuration)
  {
    _apiSecretKey = configuration["Stripe:SecretKey"] ?? throw new InvalidOperationException("Stripe Api secret not configured.");
    _webhookSecret = configuration["Stripe:WebhookSecret"] ?? throw new InvalidOperationException("Stripe Webhook secret not configured.");
  }

  public async Task<string> CreatePaymentIntentAsync(int orderId, decimal amount, CancellationToken cancellationToken)
  {
    StripeConfiguration.ApiKey = _apiSecretKey;

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

  public async Task HandleWebhookAsync(HttpRequest request)
  {
    var json = await new StreamReader(request.Body).ReadToEndAsync();
    Event stripeEvent;

    try
    {
      stripeEvent = EventUtility.ConstructEvent(
        json,
        request.Headers["Stripe-Signature"],
        _webhookSecret
      );
    }
    catch (StripeException e)
    {
      // logger.LogError(e, "⚠️ Webhook signature verification failed."); TODO DYLAN: Add Logger
      throw new BadHttpRequestException("Invalid signature");
    }

    var parsedEvent = StripeEventTypeParser.Parse(stripeEvent.Type);

    switch (parsedEvent)
    {
      case StripeEventType.CheckoutSessionCompleted:
        // handle
        break;
      case StripeEventType.PaymentIntentPaymentFailed:
        // handle
        break;
    }
  }
}
