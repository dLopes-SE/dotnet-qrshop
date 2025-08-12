using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public sealed record CreateOrderCommand(CreateOrderRequest Request) : ICommand;