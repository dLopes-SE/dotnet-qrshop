using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Payments.Webhook;

public class PaymentWebhookCommandHandler(IPaymentService _paymentService) : ICommandHandler<PaymentWebhookCommand>
{
  public async Task<Result> Handle(PaymentWebhookCommand command, CancellationToken cancellationToken)
  {
    await _paymentService.HandleWebhookAsync(command.Request);
    return Result.Success();
  }
}
