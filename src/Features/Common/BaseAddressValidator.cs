using FluentValidation;

namespace dotnet_qrshop.Features.Common;

public class BaseAddressValidator : AbstractValidator<BaseAddress>
{
  public BaseAddressValidator()
  {
    RuleFor(a => a.FullName)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(a => a.PhoneNumber)
      .NotEmpty()
      .MaximumLength(20);

    RuleFor(a => a.AddressLine1)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(a => a.AddressLine2)
      .MaximumLength(200);

    RuleFor(a => a.PostalCode)
      .NotEmpty()
      .MaximumLength(25);

    RuleFor(a => a.City)
      .NotEmpty()
      .MaximumLength(255);

    RuleFor(a => a.Country)
      .NotEmpty()
      .Length(2)
      .Matches("^[A-Z]{2}$"); // only uppercase A-Z letters
  }
}
