using dotnet_qrshop.Features.Addresses.Commands;
using FluentValidation;

namespace dotnet_qrshop.Features.Items.Commands;

public sealed class AddressValidator : AbstractValidator<AddressRequest>
{
  public AddressValidator()
  {
    RuleFor(a => a.FullName)
      .NotNull()
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(a => a.PhoneNumber)
      .NotNull()
      .NotEmpty()
      .MaximumLength(20);

    RuleFor(a => a.AddressLine1)
      .NotNull()
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(a => a.AddressLine2)
      .MaximumLength(200);

    RuleFor(a => a.PostalCode)
      .NotNull()
      .NotEmpty()
      .MaximumLength(25);

    RuleFor(a => a.City)
      .NotNull()
      .NotEmpty()
      .MaximumLength(255);

    RuleFor(a => a.Country)
      .NotNull()
      .NotEmpty()
      .Length(2)
      .Matches("^[A-Z]{2}$"); // only uppercase A-Z letters
  }
}
