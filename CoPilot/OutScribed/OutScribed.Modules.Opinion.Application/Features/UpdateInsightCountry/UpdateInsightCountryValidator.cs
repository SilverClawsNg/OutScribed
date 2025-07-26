using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightCountry
{
    public class UpdateInsightCountryValidator : Validator<UpdateInsightCountryRequest>
    {
        public UpdateInsightCountryValidator()
        {
            RuleFor(c => c.Id)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Insight is required.")
                   .NotEmpty().WithMessage("Insight is required.");

            RuleFor(c => c.Country)
                  .Cascade(CascadeMode.Stop)
                  .NotNull().WithMessage("Country is required.")
                  .NotEmpty().WithMessage("Country is required.")
                  .IsInEnum().WithMessage("Country is invalid.");
        }
    }
}
