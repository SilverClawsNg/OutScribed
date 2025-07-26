using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.FlagTaleComment
{
    public class FlagTaleCommentCommandValidator : AbstractValidator<FlagTaleCommentCommand>
    {
        public FlagTaleCommentCommandValidator()
        {

            RuleFor(c => c.TaleId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Tale is required.")
            .NotEmpty().WithMessage("Tale is required.");

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
