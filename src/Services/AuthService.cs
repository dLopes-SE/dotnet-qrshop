using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Common.Results;
using dotnet_qrshop.Domains;
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
      return Result.Failure<AuthResponse>(Error.Problem("Incorrect email/password", "Wrong email or password."));
    }

    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    if (!result.Succeeded)
    {
      return Result.Failure<AuthResponse>(Error.Problem("Incorrect email/password", "Wrong email or password."));
    }

    return Result.Success(AuthResponse.Parse(user, await _tokenProvider.Create(user)));
  }

  public async Task<Result<AuthResponse>> Registration(RegistrationRequest request)
  {
    var user = new ApplicationUser
    {
      Name = request.Name,
      Email = request.Email,
      UserName = request.Email,
      EmailConfirmed = true, // TODO DYLAN: IMPLEMENT EMAIL CONFIRMATION
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, "User"); // TODO DYLAN: ADD ROLE ENUMERATOR
      return Result.Success(AuthResponse.Parse(user));
    };

    return Result.Failure<AuthResponse>(
      Error.Problem("", string.Join("\n", result.Errors)) // TODO DYLAN: ADD USER ERRORS STATIC CLASS
    );
  }
}
