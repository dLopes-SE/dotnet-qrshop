namespace dotnet_qrshop.Common.Models;

public class RedlockSettings
{
  public TimeSpan Expiry { get; set; }
  public TimeSpan Wait { get; set; }
  public TimeSpan Retry { get; set; }
}
