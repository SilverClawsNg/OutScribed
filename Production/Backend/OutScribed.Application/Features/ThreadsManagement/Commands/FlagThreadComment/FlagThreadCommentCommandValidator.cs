using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.FlagThreadComment
{
    public class FlagThreadCommentCommandValidator : AbstractValidator<FlagThreadCommentCommand>
    {
        public FlagThreadCommentCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

            RuleFor(c => c.CommentId)
           .Cascade(CascadeMode.Stop)
           .NotNull().WithMessage("Comment is required.")
           .NotEmpty().WithMessage("Comment is required.");

            RuleFor(c => c.UserId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("User is required.")
            .NotEmpty().WithMessage("User is required.");

            RuleFor(c => c.FlagType)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Type is required.")
          .NotEmpty().WithMessage("Type is required.")
          .IsInEnum().WithMessage("Invalid type.");
        }
    }
}
