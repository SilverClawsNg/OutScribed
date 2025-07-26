using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.RateTaleComment
{
    public class RateTaleCommentCommandValidator : AbstractValidator<RateTaleCommentCommand>
    {
        public RateTaleCommentCommandValidator()
        {

            RuleFor(c => c.TaleId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Tale is required.")
            .NotEmpty().WithMessage("Tale is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

            RuleFor(c => c.RateType)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Type is required.")
          .NotEmpty().WithMessage("Type is required.")
          .IsInEnum().WithMessage("Invalid type.");

            RuleFor(c => c.CommentId)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Comment is required.")
          .NotEmpty().WithMessage("Comment is required.");
        }
    }
}
