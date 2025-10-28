namespace dotnet_qrshop.Common.Enums;

// Some of these might not be used in this specific app
public enum OrderStatusEnum
{
  None = 0,               // No checkout started
  CheckoutPending = 1,    // Checkout started but not yet paid (can still be updated)
  PaymentProcessing = 2,  // Payment is being processed
  PaymentFailed = 3,      // Payment attempt failed
  Paid = 4,               // Payment confirmed, not being processed yet
  Processing = 5,         // Being prepared / packed
  Shipped = 6,            // Handed over to courier
  Delivered = 7,          // Customer received the package
  Cancelled = 8,          // Cancelled before shipping
  Returned = 9,           // Returned by customer
  Refunded = 10           // Refund issued
}