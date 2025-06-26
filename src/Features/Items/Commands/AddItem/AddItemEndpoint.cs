using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_qrshop.Features.Items.Commands.AddItem;

public class AddItemEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("item", async (
      ItemRequest request,
      ICommandHandler<AddItemCommand, ItemDto> handler,
      CancellationToken cancellationToken) =>
    {
      var result = await handler.Handle(new AddItemCommand(request), cancellationToken);

      return result.Match(Results.Ok, CustomResults.Problem);
    })
      .WithName("AddItem")
      .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
  }
}
