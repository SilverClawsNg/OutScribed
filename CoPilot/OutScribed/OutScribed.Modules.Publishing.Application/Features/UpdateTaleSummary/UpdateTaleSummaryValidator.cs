using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleSummary
{
    public class UpdateTaleSummaryValidator : Validator<UpdateTaleSummaryRequest>
    {
        public UpdateTaleSummaryValidator()
        {

            RuleFor(c => c.Id)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Required parameter is missing.")
                   .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Summary)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Summary is required")
                      .NotEmpty().WithMessage("Summary is required")
                      .Length(3, 512).WithMessage("Summary should be between 3 and 512 chars.");

        }
    }
}
