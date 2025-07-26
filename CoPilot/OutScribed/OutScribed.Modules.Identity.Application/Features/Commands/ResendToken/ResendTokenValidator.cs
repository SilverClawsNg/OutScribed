using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ResendToken
{
    public class ResendTokenValidator : Validator<ResendTokenRequest>
    {
        public ResendTokenValidator()
        {

            RuleFor(c => c.EmailAddress)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Email address is required")
               .NotEmpty().WithMessage("Email address is required")
               .Length(3, 255).WithMessage("Email address should be between 3 and 255 chars.")
               .EmailAddress().WithMessage("Invalid email address");
        }
    }
}
