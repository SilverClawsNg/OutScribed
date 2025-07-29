using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.RemoveWatchlist
{
    public class RemoveWatchlistValidator : Validator<RemoveWatchlistRequest>
    {
        public RemoveWatchlistValidator()
        {
            RuleFor(x => x.WatchlistId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

        }
    }
}
