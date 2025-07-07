using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Items.Commands.Add;

public sealed record AddItemCommand(ItemRequest Request) : ICommand<ItemDto>;