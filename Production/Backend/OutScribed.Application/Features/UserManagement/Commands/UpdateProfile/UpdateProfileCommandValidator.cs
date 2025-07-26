using FluentValidation;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {


            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

            RuleFor(c => c.Title)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Title is required")
        .NotEmpty().WithMessage("Title is required")
        .MaximumLength(128).WithMessage("Title must not exceed 128 chars.");

            RuleFor(c => c.Bio)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Bio is required")
          .NotEmpty().WithMessage("Bio is required")
          .MaximumLength(512).WithMessage("Bio must not exceed 512 chars.");

        }
    }
}
