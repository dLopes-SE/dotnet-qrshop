using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
using dotnet_qrshop.Features.Common;
using dotnet_qrshop.Infrastructure.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace dotnet_qrshop.Features.Orders.Commands.Add;

public class CreateOrderCommandHandler(
  ApplicationDbContext _dbContext,
  IUserContext _userContext,
  IOrderService _orderService) : ICommandHandler<CreateOrderCommand>
{
  public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    if (command.Request.AddressId is not null && command.Request.AddressRequest is not null)
    {
      return Result.Failure(Error.Problem("Only one of the two properties must be present: addressId; addressRequest", "Error creating order, please try again or contact the support"));
    }

    if (await _orderService.HasPendingCheckout(cancellationToken))
    {
      return Result.Failure(Error.Problem("There's a pending checkout", "Error creating order, please try again or contact the support"));
    }

    if (command.Request.AddressId is not null && command.Request.AddressId > 0)
    {
      return await AddUserAddressOrder((int)command.Request.AddressId, cancellationToken);
    }

    if (command.Request.AddressRequest is not null)
    {
      return await AddCustomAddressOrder(command.Request.AddressRequest, cancellationToken);
    }

    return Result.Failure(Error.Failure("CreateOrder validation failure", "Error creating order, please try again or contact the support"));
  }

  private async Task<Result> AddUserAddressOrder(int addressId, CancellationToken cancellationToken)
  {
    var cartAddress = await _dbContext.Cart
      .AsNoTracking()
      .Where(c => c.UserId == _userContext.UserId)
      .Include(c => c.Items)
      .Select(c => new CartAddress
      {
        Cart = c,
        Address = c.User.Addresses
              .FirstOrDefault(a => a.Id == addressId)
      })
      .FirstOrDefaultAsync(cancellationToken);

    if (cartAddress?.Cart is null)
    {
      return Result.Failure(Error.Failure("Cart not found", "Error creating order, please try again or contact the support"));
    }

    if (cartAddress.Address is null)
    {
      return Result.Failure(Error.NotFound("Address not found", "Error creating order, please try again or contact the support"));
    }

    return await AddOrder(cartAddress.Cart, cartAddress.Address, cancellationToken);
  }

  private async Task<Result> AddCustomAddressOrder(BaseAddressRequest addressRequest, CancellationToken cancellationToken)
  {
    if (addressRequest is null)
    {
      return Result.Failure(Error.Problem("Address not provided", "Error creating order, please try again or contact the support"));
    }

    // isFavourite doesn't matter for the order's address
    var address = Address.Parse(addressRequest, isFavourite: false);

    var cart = await _dbContext.Cart
      .Include(c => c.Items)
      .FirstOrDefaultAsync(c => c.UserId == _userContext.UserId, cancellationToken);

    if (cart is null)
    {
      return Result.Failure(Error.Failure("Cart not found", "Error creating order, please try again or contact the support"));
    }

    return await AddOrder(cart, address, cancellationToken);
  }

  private async Task<Result> AddOrder(Cart cart, Address address, CancellationToken cancellationToken)
  {
    var order = Order.Create(cart, address);
    await _dbContext.Orders.AddAsync(order, cancellationToken);

    var result = await _dbContext.SaveChangesAsync(cancellationToken);
    if (result <= 0)
    {
      return Result.Failure(Error.Failure("Error inserting order", "Error creating order, please try again or contact the support"));
    }

    return Result.Success();
  }

  private record CartAddress
  {
    public Cart? Cart { get; init; }
    public Address? Address { get; init; }
  }
}
