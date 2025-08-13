namespace dotnet_qrshop.Common.Enums;

// Some of these might not be used in this specific app
public enum OrderStatusEnum
{
  Pending = 0,        // Order placed, awaiting payment confirmation
  Paying = 1,         // Processing Payment
  PaymentFailed = 2,  // Payment attempt failed
  Paid = 3,           // Payment confirmed, not being processed yet
  Processing = 4,     // Being prepared / packed
  Shipped = 5,        // Handed over to courier
  Delivered = 6,      // Customer received the package
  Cancelled = 7,      // Cancelled before shipping
  Returned = 8,       // Returned by customer
  Refunded = 9        // Refund issued
}