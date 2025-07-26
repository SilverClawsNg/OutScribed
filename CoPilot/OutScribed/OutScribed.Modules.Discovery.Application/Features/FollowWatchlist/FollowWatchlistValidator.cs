using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.FollowWatchlist
{
    public class FollowWatchlistValidator : Validator<FollowWatchlistRequest>
    {
        public FollowWatchlistValidator()
        {

            RuleFor(c => c.WatchlistId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

        }
    }
}
