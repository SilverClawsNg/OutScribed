using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.FlagInsight
{
    public class FlagInsightValidator : Validator<FlagInsightRequest>
    {
        public FlagInsightValidator()
        {

            RuleFor(c => c.InsightId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Type)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Flag type is required.")
                       .NotEmpty().WithMessage("Flag type is required.")
                       .IsInEnum().WithMessage("Flag type is invalid.");

        }
    }
}
