using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.FollowInsight
{
    public class FollowInsightValidator : Validator<FollowInsightRequest>
    {
        public FollowInsightValidator()
        {

            RuleFor(c => c.InsightId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

        }
    }
}
