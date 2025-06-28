using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Items.Queries.ListItems;

public class ListItemsEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("shop/item", async (
      IQueryHandler<ListItemsQuery, IEnumerable<ItemDto>> handler,
      CancellationToken cancellationToken,
      [FromQuery] bool featuredItemsOnly = false) =>
    {
      var result = await handler.Handle(new ListItemsQuery(featuredItemsOnly), cancellationToken);

      return result.Match(value =>
      {
        return value.Any() ? Results.Ok(value) : Results.NoContent();
      }, CustomResults.Problem);
    })
      .WithName("ListItems");
  }
}
