using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Discovery.Application.Features.RateInsightComment
{
    public class RateInsightCommentValidator : Validator<RateInsightCommentRequest>
    {
        public RateInsightCommentValidator()
        {

            RuleFor(c => c.InsightId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.CommentId)
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
