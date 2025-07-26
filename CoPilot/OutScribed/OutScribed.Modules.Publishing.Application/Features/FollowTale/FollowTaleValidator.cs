using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.FollowTale
{
    public class FollowTaleValidator : Validator<FollowTaleRequest>
    {
        public FollowTaleValidator()
        {

            RuleFor(c => c.TaleId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

        }
    }
}
