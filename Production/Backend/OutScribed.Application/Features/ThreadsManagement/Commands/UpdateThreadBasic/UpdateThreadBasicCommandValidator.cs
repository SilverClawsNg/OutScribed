using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadBasic;

public class UpdateThreadBasicCommandValidator : AbstractValidator<UpdateThreadBasicCommand>
{
    public UpdateThreadBasicCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");

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
