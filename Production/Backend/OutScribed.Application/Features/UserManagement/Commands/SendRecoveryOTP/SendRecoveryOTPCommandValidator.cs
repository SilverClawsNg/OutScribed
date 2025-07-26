using FluentValidation;

namespace OutScribed.Application.Features.UserManagement.Commands.SendRecoveryOTP
{
    public class SendRecoveryOTPCommandValidator : AbstractValidator<SendRecoveryOTPCommand>
    {
        public SendRecoveryOTPCommandValidator()
        {

            RuleFor(c => c.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(24).WithMessage("Username must not exceed 24 chars.");

        }

    }
}
