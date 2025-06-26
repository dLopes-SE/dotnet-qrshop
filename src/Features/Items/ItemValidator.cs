using FluentValidation;

namespace dotnet_qrshop.Features.Items;

internal sealed class ItemValidator : AbstractValidator<ItemRequest>
{
  public ItemValidator()
  {
    RuleFor(i => i.Name)
        .NotEmpty().WithMessage("Name is required.")
        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

    RuleFor(i => i.Slogan)
        .NotEmpty().WithMessage("Slogan is required.")
        .MaximumLength(200).WithMessage("Slogan must not exceed 200 characters.");

    RuleFor(i => i.Description)
        .NotEmpty().WithMessage("Description is required.")
        .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

    RuleFor(i => i.Image)
        .NotEmpty().WithMessage("Image is required.");

    RuleFor(i => i.IsFeaturedItem)
        .NotNull().WithMessage("IsFeatureItem must be specified.");
  }
}