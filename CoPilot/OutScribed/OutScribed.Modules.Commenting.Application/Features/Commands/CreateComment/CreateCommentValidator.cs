using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Commenting.Application.Features.Commands.CreateComment
{
    public class CreateCommentValidator : Validator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.ContentId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(c => c.ContentType)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Content type is required.")
                    .NotEmpty().WithMessage("Content type is required.")
                    .IsInEnum().WithMessage("Content type is invalid.");

            RuleFor(c => c.Details)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Details is required")
                      .NotEmpty().WithMessage("Details is required")
                      .Length(6, 1024).WithMessage("Details should be between 6 and 1024 chars.");

        }
    }
}
