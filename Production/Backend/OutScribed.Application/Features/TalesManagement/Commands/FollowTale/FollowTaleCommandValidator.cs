using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.FollowTale
{
    public class FollowTaleCommandValidator : AbstractValidator<FollowTaleCommand>
    {
        public FollowTaleCommandValidator()
        {

            RuleFor(c => c.TaleId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Tale is required.")
            .NotEmpty().WithMessage("Tale is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

        }
    }
}
