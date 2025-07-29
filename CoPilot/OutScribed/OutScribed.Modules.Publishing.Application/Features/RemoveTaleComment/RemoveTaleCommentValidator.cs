using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.RemoveTaleComment
{
    public class RemoveTaleCommentValidator : Validator<RemoveTaleCommentRequest>
    {
        public RemoveTaleCommentValidator()
        {
            RuleFor(x => x.TaleId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(x => x.CommentId)
                 .NotNull().WithMessage("Required parameter is missing.")
                 .NotEmpty().WithMessage("Required parameter is missing..");

        }
    }
}
