using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ResetPassword
{
    public class ResetPasswordValidator : Validator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {

            RuleFor(c => c.EmailAddress)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Email address is required")
                   .NotEmpty().WithMessage("Email address is required")
                   .Length(3, 255).WithMessage("Email address should be between 3 and 255 chars.")
                   .EmailAddress().WithMessage("Email address is invalid");

            RuleFor(c => c.Password)
               .NotNull().WithMessage("Password is required.")
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Token is required")
                .NotEmpty().WithMessage("Token is required")
                .InclusiveBetween(100000, 999999)
                .WithMessage("Token must be a 6-digit number.");
        }
    }
}
