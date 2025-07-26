using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.CreateThreadResponse
{
    public class CreateThreadResponseCommandValidator : AbstractValidator<CreateThreadResponseCommand>
    {
        public CreateThreadResponseCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

            RuleFor(c => c.ParentId)
           .Cascade(CascadeMode.Stop)
           .NotNull().WithMessage("Comment is required.")
           .NotEmpty().WithMessage("Comment is required.");

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

            RuleFor(c => c.CommentatorId)
       .Cascade(CascadeMode.Stop)
       .NotNull().WithMessage("Commentator is required.")
       .NotEmpty().WithMessage("Commentator is required.");

            RuleFor(c => c.Details)
                .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Details is required.")
               .NotEmpty().WithMessage("Details is required")
               .MaximumLength(1024).WithMessage("Details must not exceed 1024 chars.");
        }
    }
}
