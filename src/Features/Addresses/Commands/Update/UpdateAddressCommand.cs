

using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public record UpdateAddressCommand(int Id, AddressRequest request) : ICommand
{
  public static UpdateAddressCommand Parse(int id, AddressRequest address) => new(id, address);
}
