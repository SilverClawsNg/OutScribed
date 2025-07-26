using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ApproveWriter
{
    public class ApproveWriterValidator : Validator<ApproveWriterRequest>
    {

        public ApproveWriterValidator()
        {
            RuleFor(c => c.AccountId)
                         .Cascade(CascadeMode.Stop)
                         .NotNull().WithMessage("Account is required")
                         .NotEmpty().WithMessage("Account is required");

        }

    }
}
