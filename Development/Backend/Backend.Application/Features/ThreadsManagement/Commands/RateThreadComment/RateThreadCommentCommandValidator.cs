using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThreadComment
{
    public class RateThreadCommentCommandValidator : AbstractValidator<RateThreadCommentCommand>
    {
        public RateThreadCommentCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

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
