using Carter;
using dotnet_qrshop.Common.Models;
using Microsoft.Extensions.Options;
using RedLockNet;

namespace dotnet_qrshop.Features.Payments.Commands.CancelPaymentIntent;

public class CancelPaymentIntentEndpoint(
  IDistributedLockFactory _lockFactory, 
  IOptions<RedlockSettings> redlockOptions) : ICarterModule
{
  private readonly RedlockSettings _lockSettings = redlockOptions.Value;
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("testredis", async () =>
    {
      using var redlock = await _lockFactory.CreateLockAsync("test", _lockSettings.Expiry, _lockSettings.Wait, _lockSettings.Retry);
      if (redlock.IsAcquired)
      {
        Console.WriteLine("Acquired Lock - test Dylan");
        await Task.Delay(10000);
      }
      else
      {
        Console.WriteLine("Didn't acquire lock");
      }
    });
  }
}
