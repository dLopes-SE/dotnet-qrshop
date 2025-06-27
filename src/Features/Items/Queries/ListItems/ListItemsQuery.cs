using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.Items.Queries.ListItems;

public sealed record ListItemsQuery(bool FeaturedItemsOnly) : IQuery<IEnumerable<ItemDto>>;
