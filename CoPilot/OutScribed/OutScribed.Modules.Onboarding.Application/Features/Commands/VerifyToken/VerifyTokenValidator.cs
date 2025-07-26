using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.VerifyToken
{

    public class VerifyTokenValidator : Validator<VerifyTokenRequest>
    {
        public VerifyTokenValidator()
        {

            RuleFor(c => c.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Email address is required")
            .NotEmpty().WithMessage("Email address is required");

            RuleFor(c => c.EmailAddress)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Email address is required")
               .NotEmpty().WithMessage("Email address is required")
               .Length(3, 255).WithMessage("Email address should be between 3 and 255 chars.")
               .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Token is required")
                .NotEmpty().WithMessage("Token is required")
                 .Length(6, 6).WithMessage("Token must be a 6-digit value.");

        }
    }

}
