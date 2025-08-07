using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Addresses.Commands.Add;

public record AddAddressCommand(AddressRequest Request) : ICommand;
