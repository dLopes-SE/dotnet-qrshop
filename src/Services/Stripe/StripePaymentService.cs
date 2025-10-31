using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Common.Results;
using Stripe;

namespace dotnet_qrshop.Services.Stripe;

public class StripePaymentService : IPaymentService
{
  private readonly string _apiSecretKey;
  private readonly string _webhookSecret;
  private PaymentIntentService _paymentIntents;

  public StripePaymentService(IConfiguration configuration)
  {
    _apiSecretKey = configuration["Stripe:SecretKey"] ?? throw new InvalidOperationException("Stripe Api secret not configured.");
    _webhookSecret = configuration["Stripe:WebhookSecret"] ?? throw new InvalidOperationException("Stripe Webhook secret not configured.");
    StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
    _paymentIntents = new PaymentIntentService();
  }

  public async Task<Result<(string, string)>> CreatePaymentIntent(int orderId, decimal amount, CancellationToken cancellationToken)
  {
    StripeConfiguration.ApiKey = _apiSecretKey;

    var options = new PaymentIntentCreateOptions
    {
      Amount = (long)(amount * 100), // Stripe expects amount in cents
      Currency = "usd",
      Metadata = new Dictionary<string, string> { { "orderId", orderId.ToString() } }
    };

    var paymentIntent = await _paymentIntents.CreateAsync(options, cancellationToken: cancellationToken);
    if (paymentIntent is null)
    {
      return Result.Failure<(string, string)>(Error.Problem("Couldn't create payment intent", "Error processing payment, please try again or contact the support"));
    }

    return Result.Success((paymentIntent.Id, paymentIntent.ClientSecret));
  }

  public async Task<Result<string>> GetExistingPaymentIntent(string stripePaymentIntentId, CancellationToken cancellationToken)
  {
    var existingIntent = await _paymentIntents.GetAsync(stripePaymentIntentId, cancellationToken: cancellationToken);
    if (existingIntent is null)
    {
      return Result.Failure<string>(Error.Problem("Error retriving existing paymentIntent", "Error processing payment, please try again or contact the support"));
    }

    var status = StripeStatusTypeParse.Parse(existingIntent.Status);
    if (status is StripeStatusEnum.Unknown)
    {
      return Result.Failure<string>(Error.Problem("Unknown stripe intent's status", "Error processing payment, please try again or contact the support"));
    }

    return existingIntent.ClientSecret;
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
        _webhookSecret,
        throwOnApiVersionMismatch: false
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
      case StripeEventTypeEnum.CheckoutSessionCompleted:
        // handle
        break;
      case StripeEventTypeEnum.PaymentIntentPaymentFailed:
        // handle
        break;
    }
  }
}
