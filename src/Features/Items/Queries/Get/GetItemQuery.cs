using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Items.Queries.Get;

public sealed record GetItemQuery(int Id) : IQuery<GetItemDto>;