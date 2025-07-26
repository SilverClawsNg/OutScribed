using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.LoginAccount
{
    public class LoginAccountValidator : Validator<LoginAccountRequest>
    {
        public LoginAccountValidator()
        {
            RuleFor(c => c.Username)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Username is required.")
               .NotEmpty().WithMessage("Username is required.");

            RuleFor(c => c.Password)
               .NotNull().WithMessage("Password is required.")
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(8).WithMessage("Password must be at least 8 chars.");
        }
    }
}
