using dotnet_qrshop.Abstractions;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace dotnet_qrshop.Services;

public class AuthService(
  UserManager<ApplicationUser> _userManager, 
  SignInManager<ApplicationUser> _signInManager,
  IOptions<JwtSettings> jwtSettings) : IAuthService
{
  public Task<Result<AuthResponse>> Login(AuthRequest request)
  {
    throw new NotImplementedException();
  }

  public async Task<Result<AuthResponse>> Registration(RegistrationRequest request)
  {
    var user = new ApplicationUser
    {
      FirstName = request.FirstName,
      LastName = request.LastName,
      Email = request.Email,
      UserName = request.UserName,
      EmailConfirmed = true, // TODO DYLAN: IMPLEMENT EMAIL CONFIRMATION
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, "User"); // TODO DYLAN: ADD ROLE ENUMERATOR
      return Result.Success(new AuthResponse
      {
        Id = user.Id,
        Email = user.Email,
        UserName = user.UserName,
      });
    };

    return Result.Failure<AuthResponse>(
      Error.Failure("", string.Join("\n", result.Errors)) // TODO DYLAN: ADD USER ERRORS STATIC CLASS
    );
  }
}
