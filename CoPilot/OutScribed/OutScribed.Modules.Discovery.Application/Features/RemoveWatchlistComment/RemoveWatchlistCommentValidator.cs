using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.RemoveWatchlistComment
{
    public class RemoveWatchlistCommentValidator : Validator<RemoveWatchlistCommentRequest>
    {
        public RemoveWatchlistCommentValidator()
        {
            RuleFor(x => x.WatchlistId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(x => x.CommentId)
                 .NotNull().WithMessage("Required parameter is missing.")
                 .NotEmpty().WithMessage("Required parameter is missing..");

        }
    }
}
