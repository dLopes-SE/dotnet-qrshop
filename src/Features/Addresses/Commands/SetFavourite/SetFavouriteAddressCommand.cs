using Carter;
using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Addresses.Commands.SetFavourite;

public record SetFavouriteAddressCommand(int Id) : ICommand
{
  public static explicit operator SetFavouriteAddressCommand(int id) => new(id);
}
