using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.CreateWatchlist
{
    public class CreateWatchlistValidator : Validator<CreateWatchlistRequest>
    {
        public CreateWatchlistValidator()
        {
            RuleFor(x => x.Summary)
                .NotNull().WithMessage("Summary is required.")
                .NotEmpty().WithMessage("Summary is required.")
                .MaximumLength(512).WithMessage("Summary must not exceed 512 characters.");

            RuleFor(x => x.SourceText)
                .NotEmpty().WithMessage("Source text is required.")
                .MaximumLength(28).WithMessage("Source text must not exceed 28 characters.");

            RuleFor(x => x.SourceUrl)
                 .NotNull().WithMessage("Source URL is required.")
                .NotEmpty().WithMessage("Source URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .MaximumLength(28).WithMessage("Source URL must not exceed 255 characters.")
                .WithMessage("Source URL must be a valid absolute URL.");

            RuleFor(x => x.Category)
                .NotNull().WithMessage("Category is required.")
                .NotEmpty().WithMessage("Category is required.")
                .IsInEnum().WithMessage("Category is invalid.");

            RuleFor(x => x.Country)
                .IsInEnum().WithMessage("Country is invalid.");
        }
    }
}
