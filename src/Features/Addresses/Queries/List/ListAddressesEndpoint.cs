using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Addresses.Queries.List;

public class ListAddressesEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("user/address", async (
      IQueryHandler<ListAddressesQuery, IEnumerable<AddressDto>> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new ListAddressesQuery(), cancellationToken);

      return result.Match(
        value => value.Any() ? Results.Ok(value) : Results.NoContent(),
        CustomResults.Problem);
    })
      .WithName("ListAddresses")
      .RequireAuthorization();
  }
}
