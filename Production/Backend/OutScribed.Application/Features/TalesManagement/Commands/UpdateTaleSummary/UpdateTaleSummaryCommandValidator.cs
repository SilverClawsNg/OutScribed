using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleSummary;

public class UpdateTaleSummaryCommandValidator : AbstractValidator<UpdateTaleSummaryCommand>
{
    public UpdateTaleSummaryCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Summary)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Summary is required")
     .NotEmpty().WithMessage("Summary is required")
     .MaximumLength(512).WithMessage("Summary must not exceed 512 chars.");

    }
}
