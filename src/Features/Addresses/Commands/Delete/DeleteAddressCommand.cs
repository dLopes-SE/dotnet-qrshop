using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Addresses.Commands.Delete;

public sealed record DeleteAddressCommand(int Id) : ICommand
{
  public static explicit operator DeleteAddressCommand(int Id) => new(Id);
}
