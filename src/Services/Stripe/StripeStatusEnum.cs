namespace dotnet_qrshop.Services.Stripe;

public enum StripeStatusEnum
{
  Unknown,
  RequiresPayment,
  RequiresConfirmation,
  Processing
}
