using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Abstractions;

public interface IOrderService
{
  Result<Task<bool>> IsCartChangeAllowed();
  Task<bool> HasPendingCheckout(CancellationToken cancellationToken);
}
