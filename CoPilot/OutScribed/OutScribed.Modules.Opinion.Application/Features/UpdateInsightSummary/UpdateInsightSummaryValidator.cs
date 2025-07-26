using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightSummary
{
    public class UpdateInsightSummaryValidator : Validator<UpdateInsightSummaryRequest>
    {
        public UpdateInsightSummaryValidator()
        {
            RuleFor(c => c.Id)
                     .Cascade(CascadeMode.Stop)
                     .NotNull().WithMessage("Insight is required.")
                     .NotEmpty().WithMessage("Insight is required.");

            RuleFor(c => c.Summary)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Summary is required")
                      .NotEmpty().WithMessage("Summary is required")
                      .Length(3, 512).WithMessage("Summary should be between 3 and 512 chars.");
        }
    }
}
