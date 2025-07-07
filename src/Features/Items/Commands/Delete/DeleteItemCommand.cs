using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Items.Commands.Delete;

public sealed record DeleteItemCommand(int Id) : ICommand;
