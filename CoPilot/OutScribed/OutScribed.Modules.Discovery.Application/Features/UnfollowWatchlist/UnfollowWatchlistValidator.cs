using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.UnfollowWatchlist
{
    public class UnfollowWatchlistValidator : Validator<UnfollowWatchlistRequest>
    {
        public UnfollowWatchlistValidator()
        {

            RuleFor(c => c.WatchlistId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

        }
    }
}
