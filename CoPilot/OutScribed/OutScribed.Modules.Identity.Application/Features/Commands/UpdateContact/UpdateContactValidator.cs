using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateContact
{
    public class UpdateContactValidator : Validator<UpdateContactRequest>
    {
        public UpdateContactValidator()
        {
            RuleFor(c => c.Title)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Title is required")
                    .NotEmpty().WithMessage("Title is required")
                    .MaximumLength(56).WithMessage("Title must not exceed 56 chars.");

            RuleFor(c => c.ContactType)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Contact type is required.")
                    .NotEmpty().WithMessage("Contact type is required.")
                    .IsInEnum().WithMessage("Contact type is invalid.");

        }
    }
}
