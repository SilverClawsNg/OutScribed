using FluentValidation;

namespace Backend.Application.Features.TempUserManagement.Commands.VerifyOTPPhone;

public class VerifyOTPPhoneCommandValidator : AbstractValidator<VerifyOTPPhoneCommand>
{
    public VerifyOTPPhoneCommandValidator()
    {

        RuleFor(c => c.PhoneNumber)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("Phone number is required")
           .MaximumLength(24).WithMessage("Phone number must not exceed 24 chars.");

        RuleFor(c => c.Otp)
          .NotNull().WithMessage("One-time password is required.")
          .NotEmpty().WithMessage("One-time password is required.");
    }
}
