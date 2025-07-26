using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateProfile
{

    public class UpdateProfileValidator : Validator<UpdateProfileRequest>
    {

        public UpdateProfileValidator()
        {

            RuleFor(c => c.EmailAddress)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Email address is required")
                    .NotEmpty().WithMessage("Email address is required")
                    .Length(3, 255).WithMessage("Email address should be between 3 and 255 chars.")
                    .EmailAddress().WithMessage("Email address is invalid");

            RuleFor(c => c.Title)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Title is required")
                   .NotEmpty().WithMessage("Title is required")
                   .Length(3, 128).WithMessage("Title should be between 3 and 128 chars.");

            RuleFor(c => c.Bio)
                 .Cascade(CascadeMode.Stop)
                 .NotNull().WithMessage("Bio is required")
                 .NotEmpty().WithMessage("Bio is required")
                 .Length(3, 512).WithMessage("Bio should be between 3 and 512 chars.");

        }
    }
}
