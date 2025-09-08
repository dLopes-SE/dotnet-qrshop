using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Addresses.Commands.Update;

public record UpdateAddressRequest (BaseAddress Address, bool IsFavourite);