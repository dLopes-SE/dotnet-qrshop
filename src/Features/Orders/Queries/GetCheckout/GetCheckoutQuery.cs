using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Orders.Queries.GetCheckout;

public sealed record GetCheckoutQuery : IQuery<CheckoutDto>;
