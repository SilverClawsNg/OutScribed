using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightComment
{
    public class UpdateInsightCommentValidator : Validator<UpdateInsightCommentRequest>
    {
        public UpdateInsightCommentValidator()
        {
            RuleFor(x => x.InsightId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(x => x.CommentId)
                 .NotNull().WithMessage("Required parameter is missing.")
                 .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(c => c.Text)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Text is required")
                      .NotEmpty().WithMessage("Text is required")
                      .Length(6, 1024).WithMessage("Text should be between 6 and 1024 chars.");

        }
    }
}
