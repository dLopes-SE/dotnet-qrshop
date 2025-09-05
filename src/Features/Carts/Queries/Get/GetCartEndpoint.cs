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
    app.MapGet("shop/cart", async (
      HttpContext _context,
      IQueryHandler<GetCartQuery, CartDto> handler, 
      CancellationToken cancellationToken,
      [FromQuery] bool isCartPreview = false) =>
    {
      var cartHash = _context.Request.Cookies["cart_hash"];

      var result = await handler.Handle(new GetCartQuery(isCartPreview), cancellationToken);
      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetCart")
      .RequireAuthorization();
  }
}
