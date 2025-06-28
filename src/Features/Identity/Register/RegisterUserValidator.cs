using FluentValidation;

namespace dotnet_qrshop.Features.Identity.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
  public RegisterUserValidator()
  {
    RuleFor(u => u.Request.Name)
      .NotNull()
      .NotEmpty()
      .WithMessage("Must enter a name.");

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
