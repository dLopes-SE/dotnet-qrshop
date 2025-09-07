namespace dotnet_qrshop.Common.Enums;

// Some of these might not be used in this specific app
public enum OrderStatusEnum
{
  None = 0,           // Indicates that there's no 'active' checkout
  Pending = 1,        // Order placed, awaiting payment confirmation
  Paying = 2,         // Processing Payment
  PaymentFailed = 3,  // Payment attempt failed
  Paid = 4,           // Payment confirmed, not being processed yet
  Processing = 5,     // Being prepared / packed
  Shipped = 6,        // Handed over to courier
  Delivered = 7,      // Customer received the package
  Cancelled = 8,      // Cancelled before shipping
  Returned = 9,       // Returned by customer
  Refunded = 10        // Refund issued
}