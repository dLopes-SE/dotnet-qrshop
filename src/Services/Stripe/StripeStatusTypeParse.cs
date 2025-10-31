namespace dotnet_qrshop.Services.Stripe;

public class StripeStatusTypeParse
{
  public static StripeStatusEnum Parse(string statusType)
  {
    return statusType switch
    {
      "requires_payment_method" => StripeStatusEnum.RequiresPayment,
      "requires_confirmation" => StripeStatusEnum.RequiresConfirmation,
      "processing" => StripeStatusEnum.Processing,
      _ => StripeStatusEnum.Unknown
    };
  }
}