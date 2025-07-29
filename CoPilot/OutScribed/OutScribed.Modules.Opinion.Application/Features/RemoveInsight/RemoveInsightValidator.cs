using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.RemoveInsight
{
    public class RemoveInsightValidator : Validator<RemoveInsightRequest>
    {
        public RemoveInsightValidator()
        {
            RuleFor(x => x.InsightId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

        }
    }
}
