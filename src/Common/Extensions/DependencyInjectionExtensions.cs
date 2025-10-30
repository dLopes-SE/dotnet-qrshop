using dotnet_qrshop.Common.Models;
using RedLockNet;
using RedLockNet.SERedis;
using StackExchange.Redis;

namespace dotnet_qrshop.Common.Extensions;

public static class DependencyInjectionExtensions
{
  public static void AddRedisServices(this WebApplicationBuilder builder)
  {
    var configurationOptions = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379", true);
    configurationOptions.AbortOnConnectFail = false;
    configurationOptions.ConnectRetry = 3;

    builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(configurationOptions));

    builder.Services.Configure<RedlockSettings>(builder.Configuration.GetSection("Redlock"));
    builder.Services.AddSingleton<IDistributedLockFactory, RedLockFactory>(_ =>
      RedLockFactory.Create([ConnectionMultiplexer.Connect(configurationOptions)]));
  }
}
