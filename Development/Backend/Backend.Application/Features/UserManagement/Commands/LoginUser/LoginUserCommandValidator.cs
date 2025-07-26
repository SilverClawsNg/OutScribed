using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {

        public LoginUserCommandValidator()
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
