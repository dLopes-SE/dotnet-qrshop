using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Orders.Commands.UpdateAddress
{
  public sealed record UpdateOrderAddressCommand(UpdateOrderAddressRequest Request) : ICommand
  {
    public static explicit operator UpdateOrderAddressCommand(UpdateOrderAddressRequest request) => new(request);
  };
}
