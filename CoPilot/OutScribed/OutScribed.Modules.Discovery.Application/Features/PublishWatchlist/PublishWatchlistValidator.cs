using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.PublishWatchlist
{
    public class PublishWatchlistValidator : Validator<PublishWatchlistRequest>
    {
        public PublishWatchlistValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Watchlist is required.")
                .NotEmpty().WithMessage("Watchlist is required.");

            RuleFor(c => c.Confirm)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Confirmation is required")
                       .NotEmpty().WithMessage("Confirmation is required");
        }
    }

}
