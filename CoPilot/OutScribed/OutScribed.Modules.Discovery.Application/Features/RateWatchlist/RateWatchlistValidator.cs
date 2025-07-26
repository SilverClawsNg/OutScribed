using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.RateWatchlist
{
    public class RateWatchlistValidator : Validator<RateWatchlistRequest>
    {
        public RateWatchlistValidator()
        {

            RuleFor(c => c.WatchlistId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Type)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Rate type is required.")
                       .NotEmpty().WithMessage("Rate type is required.")
                       .IsInEnum().WithMessage("Rate type is invalid.");

        }
    }
}
