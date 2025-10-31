namespace dotnet_qrshop.Services.Stripe;

public static class StripeEventTypeParser
{
  public static StripeEventTypeEnum Parse(string eventType)
  {
    return eventType switch
    {
      "payment_intent.succeeded" => StripeEventTypeEnum.CheckoutSessionCompleted,
      "payment_intent.payment_failed" => StripeEventTypeEnum.PaymentIntentPaymentFailed,
      _ => StripeEventTypeEnum.Unknown
    };
  }
}
