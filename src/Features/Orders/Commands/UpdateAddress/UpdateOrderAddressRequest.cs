using dotnet_qrshop.Features.Common;

namespace dotnet_qrshop.Features.Orders.Commands.UpdateAddress;

public sealed record UpdateOrderAddressRequest(BaseAddressRequest Address, int? AddressId);
