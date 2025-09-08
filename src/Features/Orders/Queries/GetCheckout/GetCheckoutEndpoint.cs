using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Orders.Queries.GetCheckout;

public class GetCheckoutEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("order/checkout", async (
      IQueryHandler<GetCheckoutQuery, CheckoutDto> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new GetCheckoutQuery(), cancellationToken);
      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetCheckout")
      .RequireAuthorization();
  }
}
  