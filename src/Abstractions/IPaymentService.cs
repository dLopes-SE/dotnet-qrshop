namespace dotnet_qrshop.Abstractions;
public interface IPaymentService
{
  Task<string> CreatePaymentIntentAsync(int orderId, decimal ammount, CancellationToken cancellationToken);
  Task HandleWebhookAsync(HttpRequest request);
}