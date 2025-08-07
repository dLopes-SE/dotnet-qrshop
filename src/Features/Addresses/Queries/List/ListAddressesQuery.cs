using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models;

namespace dotnet_qrshop.Features.Addresses.Queries.List;

public sealed record ListAddressesQuery : IQuery<IEnumerable<AddressDto>>;
