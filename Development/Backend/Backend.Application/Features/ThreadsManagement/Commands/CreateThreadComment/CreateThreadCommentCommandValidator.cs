using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThreadComment
{
    public class CreateThreadCommentCommandValidator : AbstractValidator<CreateThreadCommentCommand>
    {
        public CreateThreadCommentCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

            RuleFor(c => c.Details)
                .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Comment is required.")
               .NotEmpty().WithMessage("Comment is required")
               .MaximumLength(1024).WithMessage("Comment must not exceed 1024 chars.");
        }
    }
}
