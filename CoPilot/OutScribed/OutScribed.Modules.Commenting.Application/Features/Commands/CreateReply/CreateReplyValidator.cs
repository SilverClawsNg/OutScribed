using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Commenting.Application.Features.Commands.CreateReply
{
    public class CreateReplyValidator : Validator<CreateReplyRequest>
    {
        public CreateReplyValidator()
        {
            RuleFor(x => x.CommentId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(c => c.Details)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Details is required")
                      .NotEmpty().WithMessage("Details is required")
                      .Length(6, 1024).WithMessage("Details should be between 6 and 1024 chars.");

        }
    }
}
