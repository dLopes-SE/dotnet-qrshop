using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Infrastructure.Database.DbContext;

namespace dotnet_qrshop.Features.Identity.Register;


public class RegisterUserCommandHandler(
  IAuthService _authService,
  ApplicationDbContext _dbContext)
  : ICommandHandler<RegisterUserCommand, RegistrationResponse>
{
  public async Task<Result<RegistrationResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
  {
    var result = await _authService.Registration(command.Request);
    if (result.IsFailure)
    {
      return Result.Failure<RegistrationResponse>(result.Error);
    }

    await AddCart(result.Value.Id, cancellationToken);

    return Result.Success(new RegistrationResponse
    {
      UserId = result.Value.Id,
    });
  }

  private async Task AddCart(Guid userId, CancellationToken cancellationToken)
  {
    var cart = new Cart
    {
      UserId = userId,
      VersionHash = string.Empty
    };

    await _dbContext.Cart.AddAsync(cart, cancellationToken);
    await _dbContext.SaveChangesAsync(cancellationToken);

    // Now we got the cardId
    cart.UpdateHashVersion();
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
}
