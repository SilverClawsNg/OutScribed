using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleCountry
{
    public class UpdateTaleCountryValidator : Validator<UpdateTaleCountryRequest>
    {
        public UpdateTaleCountryValidator()
        {

            RuleFor(c => c.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Required parameter is missing.")
                .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Country)
                  .Cascade(CascadeMode.Stop)
                  .NotNull().WithMessage("Country is required.")
                  .NotEmpty().WithMessage("Country is required.")
                  .IsInEnum().WithMessage("Country is invalid.");

        }
    }
}
