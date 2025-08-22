using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Http;

namespace dotnet_qrshop.Features.UserInfo.Queries.IsAdmin;

public class IsAdminEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("user/me/is-admin", async (
      IQueryHandler<IsAdminQuery, bool> handler, 
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new IsAdminQuery(), cancellationToken);

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("IsAdmin")
      .RequireAuthorization();
  }
}
