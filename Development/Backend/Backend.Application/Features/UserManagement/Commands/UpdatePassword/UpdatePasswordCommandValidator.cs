using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {

        public UpdatePasswordCommandValidator()
        {

            RuleFor(c => c.AccountId)
               .NotNull().WithMessage("Missing parameter.")
               .NotEmpty().WithMessage("Missing Parameter.");

            RuleFor(c => c.OldPassword)
            .NotNull().WithMessage("Old password is required.")
            .NotEmpty().WithMessage("Old password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

            RuleFor(c => c.Password)
               .NotNull().WithMessage("New password is required.")
               .NotEmpty().WithMessage("New password is required")
               .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

        }
    }
}
