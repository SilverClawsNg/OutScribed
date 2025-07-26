using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ApplyAsWriter
{
    public class ApplyAsWriterValidator : Validator<ApplyAsWriterRequest>
    {
        public ApplyAsWriterValidator()
        {

            RuleFor(c => c.Country)
                  .Cascade(CascadeMode.Stop)
                  .NotNull().WithMessage("Country is required.")
                  .NotEmpty().WithMessage("Country is required.")
                  .IsInEnum().WithMessage("Country is invalid.");

            RuleFor(c => c.Address)
                  .Cascade(CascadeMode.Stop)
                  .NotNull().WithMessage("Address is required")
                  .NotEmpty().WithMessage("Address is required")
                  .MaximumLength(255).WithMessage("Address must not exceed 255 chars.");

            RuleFor(c => c.Base64String)
                 .Cascade(CascadeMode.Stop)
                 .NotNull().WithMessage("Application profile is required")
                 .NotEmpty().WithMessage("Application profile is required");
        }
    }
}
