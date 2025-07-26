using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.AddTaleComment
{
    public class AddTaleCommentValidator : Validator<AddTaleCommentRequest>
    {
        public AddTaleCommentValidator()
        {
            RuleFor(x => x.TaleId)
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
