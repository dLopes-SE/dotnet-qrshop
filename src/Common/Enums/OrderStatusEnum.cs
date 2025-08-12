namespace dotnet_qrshop.Common.Enums;

// Some of these might not be used in this specific app
public enum OrderStatusEnum
{
  Pending = 0,        // Order placed, awaiting payment confirmation
  PaymentFailed = 1,  // Payment attempt failed
  Paid = 2,           // Payment confirmed, not yet processed
  Processing = 3,     // Being prepared / packed
  Shipped = 4,        // Handed over to courier
  Delivered = 5,      // Customer received the package
  Cancelled = 6,      // Cancelled before shipping
  Returned = 7,       // Returned by customer
  Refunded = 8        // Refund issued
}
