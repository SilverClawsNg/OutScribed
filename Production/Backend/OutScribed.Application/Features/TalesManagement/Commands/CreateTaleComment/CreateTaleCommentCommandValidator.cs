using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTaleComment
{
    public class CreateTaleCommentCommandValidator : AbstractValidator<CreateTaleCommentCommand>
    {
        public CreateTaleCommentCommandValidator()
        {

            RuleFor(c => c.TaleId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Tale is required.")
            .NotEmpty().WithMessage("Tale is required.");

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
