using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTale;

public class CreateTaleCommandValidator : AbstractValidator<CreateTaleCommand>
{
    public CreateTaleCommandValidator()
    {

        RuleFor(c => c.Title)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Title is required")
     .NotEmpty().WithMessage("Title is required")
     .MaximumLength(128).WithMessage("Title must not exceed 128 chars.");

        RuleFor(c => c.Category)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Category is required.")
          .NotEmpty().WithMessage("Category is required.")
          .IsInEnum().WithMessage("Invalid category.");
    }
}
