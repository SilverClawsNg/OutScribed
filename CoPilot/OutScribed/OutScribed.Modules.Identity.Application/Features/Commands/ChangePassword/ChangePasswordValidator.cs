using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ChangePassword
{
    public class ChangePasswordValidator : Validator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.OldPassword)
                 .NotNull().WithMessage("Old password is required.")
                 .NotEmpty().WithMessage("Old password is required")
                 .MinimumLength(8).WithMessage("Old password must be at least 8 chars.");

            RuleFor(c => c.NewPassword)
                 .NotNull().WithMessage("New password is required.")
                 .NotEmpty().WithMessage("New password is required")
                 .MinimumLength(8).WithMessage("New password must be at least 8 chars.");
        }
    }
}
