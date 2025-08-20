using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;

namespace dotnet_qrshop.Abstractions;

public interface IOrderService
{
  Task<bool> IsCartChangeAllowedAsync(CancellationToken cancellationToken);
  Task<bool> IsAddressChangeAllowedAsync(CancellationToken cancellationToken);
  Task<bool> HasPendingCheckout(CancellationToken cancellationToken);
  Task<Order> GetPendingOrder(CancellationToken cancellationToken);
  Task<Order> GetPendingOrder(int addressId,  CancellationToken cancellationToken);
}
