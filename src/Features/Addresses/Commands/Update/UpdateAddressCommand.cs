

using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public record UpdateAddressCommand(int Id, UpdateAddressRequest Request) : ICommand
{
  public static UpdateAddressCommand Parse(int id, UpdateAddressRequest address) => new(id, address);
}
