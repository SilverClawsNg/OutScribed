using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UnfollowTale
{
    public class UnfollowTaleValidator : Validator<UnfollowTaleRequest>
    {
        public UnfollowTaleValidator()
        {

            RuleFor(c => c.TaleId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

        }
    }
}
