using FluentValidation;

namespace Backend.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPPhone
{
    public class SetDoNotResendOTPPhoneCommandValidator : AbstractValidator<SetDoNotResendOTPPhoneCommand>
    {
        public SetDoNotResendOTPPhoneCommandValidator()
        {

            RuleFor(c => c.PhoneNumber)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Phone number is required")
             .MaximumLength(24).WithMessage("Phone number must not exceed 24 chars.");


        }
    }

}
