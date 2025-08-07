using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.UserInfo.Queries.Get;

public class GetUserInfoEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("user/info", async (
      IQueryHandler<GetUserInfoQuery, UserInfoDto> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new GetUserInfoQuery(), cancellationToken);

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("GetUserInfo")
      .RequireAuthorization();
  }
}
