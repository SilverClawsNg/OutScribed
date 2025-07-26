using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleText
{
    public class UpdateTaleTextValidator : Validator<UpdateTaleTextRequest>
    {
        public UpdateTaleTextValidator()
        {
            RuleFor(c => c.Id)
                     .Cascade(CascadeMode.Stop)
                     .NotNull().WithMessage("Required parameter is missing.")
                     .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Text)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Details is required")
                      .NotEmpty().WithMessage("Details is required")
                      .Length(3, 65535).WithMessage("Details should be between 3 and 65535 chars.");

        }
    }
}
