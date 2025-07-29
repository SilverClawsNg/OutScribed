using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleComment
{
    public class UpdateTaleCommentValidator : Validator<UpdateTaleCommentRequest>
    {
        public UpdateTaleCommentValidator()
        {
            RuleFor(x => x.TaleId)
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
