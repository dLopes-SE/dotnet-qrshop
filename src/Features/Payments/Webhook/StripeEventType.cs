namespace dotnet_qrshop.Features.Payments.Webhook;

public enum StripeEventType
{
  CheckoutSessionCompleted,
  PaymentIntentPaymentFailed,
  Unknown
}
