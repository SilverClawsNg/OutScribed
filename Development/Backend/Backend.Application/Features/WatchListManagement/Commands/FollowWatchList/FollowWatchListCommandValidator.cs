using FluentValidation;

namespace Backend.Application.Features.WatchListManagement.Commands.FollowWatchList
{
    public class FollowWatchListCommandValidator : AbstractValidator<FollowWatchListCommand>
    {
        public FollowWatchListCommandValidator()
        {

            RuleFor(c => c.WatchListId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("WatchList is required.")
            .NotEmpty().WithMessage("WatchList is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

        }
    }
}
