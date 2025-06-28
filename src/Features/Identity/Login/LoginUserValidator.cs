using FluentValidation;

namespace dotnet_qrshop.Features.Identity.Login;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
  public LoginUserValidator()
  {
    RuleFor(u => u.Request.Email)
      .NotNull()
      .NotEmpty()
      .EmailAddress()
      .WithMessage("Must enter a valid email");

    RuleFor(u => u.Request.Password)
      .NotNull()
      .NotEmpty()
      .WithMessage("Must enter the password");
  }
}
