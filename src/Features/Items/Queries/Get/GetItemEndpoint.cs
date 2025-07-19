using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Items.Queries.Get;

public class GetItemEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("shop/item/{id}", async (
      int id,
      IQueryHandler<GetItemQuery, GetItemDto> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new GetItemQuery(id), cancellationToken);
      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetItem");
  }
}
