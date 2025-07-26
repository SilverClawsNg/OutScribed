using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateWriter
{
    public class UpdateWriterValidator : Validator<UpdateWriterRequest>
    {

        public UpdateWriterValidator()
        {
            RuleFor(c => c.AccountId)
                         .Cascade(CascadeMode.Stop)
                         .NotNull().WithMessage("Account is required")
                         .NotEmpty().WithMessage("Account is required");

            RuleFor(c => c.IsActive)
                     .Cascade(CascadeMode.Stop)
                     .NotNull().WithMessage("Option is required")
                     .NotEmpty().WithMessage("Optipn is required");
        }

    }
}
