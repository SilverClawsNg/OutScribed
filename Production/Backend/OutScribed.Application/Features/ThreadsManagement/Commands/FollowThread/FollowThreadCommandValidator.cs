using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.FollowThread
{
    public class FollowThreadCommandValidator : AbstractValidator<FollowThreadCommand>
    {
        public FollowThreadCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

        }
    }
}
