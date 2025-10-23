namespace dotnet_qrshop.Features.Payments.Webhook;

public static class StripeEventTypeParser
{
  public static StripeEventType Parse(string eventType)
  {
    return eventType switch
    {
      "payment_intent.succeeded" => StripeEventType.CheckoutSessionCompleted,
      "payment_intent.payment_failed" => StripeEventType.PaymentIntentPaymentFailed,
      _ => StripeEventType.Unknown
    };
  }
}
