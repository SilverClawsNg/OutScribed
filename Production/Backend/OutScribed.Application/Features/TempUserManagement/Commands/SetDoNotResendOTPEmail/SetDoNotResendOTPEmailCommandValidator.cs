using FluentValidation;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPEmail
{
    public class SetDoNotResendOTPEmailCommandValidator : AbstractValidator<SetDoNotResendOTPEmailCommand>
    {
        public SetDoNotResendOTPEmailCommandValidator()
        {

            RuleFor(c => c.EmailAddress)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("Email address is required")
               .Length(3, 50).WithMessage("Email address should be between 3 and 50 chars.")
               .EmailAddress().WithMessage("Invalid email address");

        }
    }

}
