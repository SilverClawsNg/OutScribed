using FluentValidation;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPPhone
{
    public class ResendRegistrationOTPPhoneCommandValidator : AbstractValidator<ResendRegistrationOTPPhoneCommand>
    {
        public ResendRegistrationOTPPhoneCommandValidator()
        {
            RuleFor(c => c.PhoneNumber)
                       .Cascade(CascadeMode.Stop)
                       .NotEmpty().WithMessage("Phone number is required")
                       .MaximumLength(24).WithMessage("Phone number must not exceed 24 chars.");

        }

    }
}
