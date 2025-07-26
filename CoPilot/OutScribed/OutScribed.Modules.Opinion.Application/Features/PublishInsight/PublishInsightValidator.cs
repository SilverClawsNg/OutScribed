using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.PublishInsight
{
    public class PublishInsightValidator : Validator<PublishInsightRequest>
    {
        public PublishInsightValidator()
        {
            RuleFor(c => c.Id)
                         .Cascade(CascadeMode.Stop)
                         .NotNull().WithMessage("Insight is required.")
                         .NotEmpty().WithMessage("Insight is required.");

            RuleFor(c => c.Confirm)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Confirmation is required")
                       .NotEmpty().WithMessage("Confirmation is required");
        }
    }
}
