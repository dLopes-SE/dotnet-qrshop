using FluentValidation;

namespace dotnet_qrshop.Features.Items.Commands.AddItem;

internal sealed class AddItemValidator : AbstractValidator<AddItemCommand>
{
  public AddItemValidator()
  {
    RuleFor(i => i.Request)
      .SetValidator(new ItemValidator());
  }
}
