using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Models;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_qrshop.Features.Carts.Queries.Get;

public class GetCartEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("cart", async (
      HttpContext _context,
      IQueryHandler<GetCartQuery, CartDto> handler, 
      CancellationToken cancellationToken,
      [FromQuery] bool isCartPreview = false) =>
    {
      var cartHash = _context.Request.Cookies["cart_hash"];

      var result = await handler.Handle(new GetCartQuery(isCartPreview), cancellationToken);

      if (result.IsSuccess && result.Value.Quantity == 0)
      {
        return Results.NoContent();
      }

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetCart")
      .RequireAuthorization();
  }
}
