using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.CreateAccount
{
    public class CreateAccountValidator : Validator<CreateAccountRequest>
    {
        public CreateAccountValidator()
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

            RuleFor(c => c.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(20).WithMessage("Username must not exceed 20 chars.");

        }
    }
}
