using FluentValidation;

namespace OutScribed.Application.Features.UserManagement.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {

        public RefreshTokenCommandValidator()
        {

            RuleFor(c => c.Token)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("Token is required.")
               .NotEmpty().WithMessage("Token is required.");

        }
    }
}
