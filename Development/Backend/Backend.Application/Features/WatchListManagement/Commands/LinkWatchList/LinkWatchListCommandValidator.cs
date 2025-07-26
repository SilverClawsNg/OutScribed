using FluentValidation;

namespace Backend.Application.Features.WatchListManagement.Commands.LinkWatchList
{
    public class LinkWatchListCommandValidator : AbstractValidator<LinkWatchListCommand>
    {
        public LinkWatchListCommandValidator()
        {

            RuleFor(c => c.WatchListId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("WatchList is required.")
            .NotEmpty().WithMessage("WatchList is required.");

            RuleFor(c => c.TaleId)
           .Cascade(CascadeMode.Stop)
           .NotNull().WithMessage("Tale is required.")
           .NotEmpty().WithMessage("Tale is required.");
        }
    }
}
