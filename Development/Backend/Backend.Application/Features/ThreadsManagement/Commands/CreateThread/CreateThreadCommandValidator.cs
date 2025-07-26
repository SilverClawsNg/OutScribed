using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThread;

public class CreateThreadCommandValidator : AbstractValidator<CreateThreadCommand>
{
    public CreateThreadCommandValidator()
    {

        RuleFor(c => c.Title)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Title is required")
     .NotEmpty().WithMessage("Title is required")
     .MaximumLength(128).WithMessage("Title must not exceed 128 chars.");

        RuleFor(c => c.TaleId)
  .Cascade(CascadeMode.Stop)
  .NotNull().WithMessage("Tale is required")
  .NotEmpty().WithMessage("Tale is required");

        RuleFor(c => c.Category)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Category is required.")
          .NotEmpty().WithMessage("Category is required.")
          .IsInEnum().WithMessage("Invalid category.");
    }
}
