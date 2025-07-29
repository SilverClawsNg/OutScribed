using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.RemoveInsightComment
{
    public class RemoveInsightCommentValidator : Validator<RemoveInsightCommentRequest>
    {
        public RemoveInsightCommentValidator()
        {
            RuleFor(x => x.InsightId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(x => x.CommentId)
                 .NotNull().WithMessage("Required parameter is missing.")
                 .NotEmpty().WithMessage("Required parameter is missing..");

        }
    }
}
