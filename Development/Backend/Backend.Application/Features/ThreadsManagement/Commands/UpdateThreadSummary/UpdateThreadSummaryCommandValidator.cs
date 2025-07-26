using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadSummary;

public class UpdateThreadSummaryCommandValidator : AbstractValidator<UpdateThreadSummaryCommand>
{
    public UpdateThreadSummaryCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");

        RuleFor(c => c.Summary)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Summary is required")
     .NotEmpty().WithMessage("Summary is required")
     .MaximumLength(256).WithMessage("Summary must not exceed 256 chars.");

    }
}
