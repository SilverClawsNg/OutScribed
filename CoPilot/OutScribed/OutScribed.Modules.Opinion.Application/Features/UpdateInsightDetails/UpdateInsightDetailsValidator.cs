using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightDetails
{
    public class UpdateInsightDetailsValidator : Validator<UpdateInsightDetailsRequest>
    {
        public UpdateInsightDetailsValidator()
        {
            RuleFor(c => c.Id)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Insight is required.")
                       .NotEmpty().WithMessage("Insight is required.");

            RuleFor(c => c.Details)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Details is required")
                      .NotEmpty().WithMessage("Details is required")
                      .Length(3, 65535).WithMessage("Details should be between 3 and 65535 chars.");

        }
    }
}
