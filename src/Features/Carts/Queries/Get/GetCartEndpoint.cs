using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Http;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public class GetCartEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("cart", async (
      HttpContext _context,
      IQueryHandler<GetCartQuery, IEnumerable<CartItemDto>> handler, 
      CancellationToken cancellationToken) =>
    {
      var cartHash = _context.Request.Cookies["cart_hash"];

      var result = await handler.Handle(new GetCartQuery(), cancellationToken);

      if (result.IsSuccess && !result.Value.Any())
      {
        return Results.NoContent();
      }

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetCart")
      .RequireAuthorization();
  }
}
