using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Payments.Commands.CreatePaymentIntent;

public sealed record CreatePaymentIntentCommand(int OrderId) : ICommand<string>;
