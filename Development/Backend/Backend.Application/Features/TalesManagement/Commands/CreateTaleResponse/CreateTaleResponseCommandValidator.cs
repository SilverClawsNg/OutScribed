using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTaleResponse
{
    public class CreateTaleResponseCommandValidator : AbstractValidator<CreateTaleResponseCommand>
    {
        public CreateTaleResponseCommandValidator()
        {

            RuleFor(c => c.TaleId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Tale is required.")
            .NotEmpty().WithMessage("Tale is required.");

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
