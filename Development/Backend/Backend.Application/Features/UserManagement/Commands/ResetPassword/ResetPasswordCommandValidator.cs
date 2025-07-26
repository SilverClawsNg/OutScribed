using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {

        public ResetPasswordCommandValidator()
        {

            RuleFor(c => c.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User not found.")
                .NotEmpty().WithMessage("User not found.");

            RuleFor(c => c.Otp)
                 .NotNull().WithMessage("OTP is required.")
                 .NotEmpty().WithMessage("OTP is required.");

            RuleFor(c => c.Password)
               .NotNull().WithMessage("Password is required.")
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

        }
    }
}
