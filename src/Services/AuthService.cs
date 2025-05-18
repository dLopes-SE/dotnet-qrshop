using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Entities;
using Microsoft.AspNetCore.Identity;

namespace dotnet_qrshop.Services;

public class AuthService(
  UserManager<ApplicationUser> _userManager, 
  SignInManager<ApplicationUser> _signInManager,
  ITokenProvider _tokenProvider) : IAuthService
{
  public async Task<Result<AuthResponse>> Login(AuthRequest request)
  {
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null)
    {
      return Result.Failure<AuthResponse>(Error.NotFound("User Not Found", "Wrong email or password."));
    }

    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    if (!result.Succeeded)
    {
      return Result.Failure<AuthResponse>(Error.Failure("Incorrect password", "Wrong email or password."));
    }

    return Result.Success(new AuthResponse
    {
      Id = user.Id,
      UserName = user.UserName,
      Email = user.Email,
      Token = await _tokenProvider.Create(user),
    });
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
