using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Abstractions;

public interface IOrderService
{
  Task<bool> IsCartChangeAllowedAsync(CancellationToken cancellationToken);
  Task<bool> HasOnGoingCheckout(CancellationToken cancellationToken);
  Task<bool> HasPendingCheckout(CancellationToken cancellationToken);
}
