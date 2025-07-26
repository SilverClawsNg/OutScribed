using FluentValidation;

namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPEmail;

public class VerifyOTPEmailCommandValidator : AbstractValidator<VerifyOTPEmailCommand>
{
    public VerifyOTPEmailCommandValidator()
    {

        RuleFor(c => c.EmailAddress)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Email address is required")
             .Length(3, 50).WithMessage("Email address should be between 3 and 50 chars.")
             .EmailAddress().WithMessage("Invalid email address");

        RuleFor(c => c.Otp)
          .NotNull().WithMessage("One-time password is required.")
          .NotEmpty().WithMessage("One-time password is required.");
    }
}
