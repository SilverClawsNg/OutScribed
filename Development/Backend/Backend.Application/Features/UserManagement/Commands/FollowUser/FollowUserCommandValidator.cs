using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.FollowUser
{
    public class FollowUserCommandValidator : AbstractValidator<FollowUserCommand>
    {
        public FollowUserCommandValidator()
        {

            RuleFor(c => c.UserId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("User is required.")
            .NotEmpty().WithMessage("User is required.");

            RuleFor(c => c.FollowerId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

        }
    }
}
