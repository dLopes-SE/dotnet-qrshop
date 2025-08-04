using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Carts.Commands.RemoveItem;

public sealed record RemoveItemCommand(int CartItemId) : ICommand;
