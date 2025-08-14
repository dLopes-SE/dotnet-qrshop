using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Addresses.Commands;

public record AddAddressRequest (BaseAddressRequest Address, bool IsFavourite);