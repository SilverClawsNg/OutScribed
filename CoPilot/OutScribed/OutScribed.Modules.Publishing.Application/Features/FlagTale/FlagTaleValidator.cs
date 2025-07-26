using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.FlagTale
{
    public class FlagTaleValidator : Validator<FlagTaleRequest>
    {
        public FlagTaleValidator()
        {

            RuleFor(c => c.TaleId)
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
