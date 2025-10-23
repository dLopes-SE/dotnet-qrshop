using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Payments.Webhook;

public sealed record PaymentWebhookCommand(HttpRequest Request) : ICommand;
