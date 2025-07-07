using Carter;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Extensions;
using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Features.Items.Commands.Delete
{
  public class DeleteItemEndpoint : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapDelete("shop/item/{id}", async (
        int id,
        ICommandHandler<DeleteItemCommand> handler,
        CancellationToken cancellationToken) =>
      {
        var result = await handler.Handle(new DeleteItemCommand(id), cancellationToken);

        return result.Match(() => Results.Ok(), CustomResults.Problem);
      })
        .WithName("DeleteItem");
    }
  }
}
