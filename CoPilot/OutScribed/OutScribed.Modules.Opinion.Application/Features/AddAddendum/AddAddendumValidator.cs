using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.AddAddendum
{
    public class AddAddendumValidator : Validator<AddAddendumRequest>
    {
        public AddAddendumValidator()
        {
            RuleFor(x => x.InsightId)
                  .NotNull().WithMessage("Required parameter is missing.")
                  .NotEmpty().WithMessage("Required parameter is missing..");

            RuleFor(c => c.Text)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Text is required")
                      .NotEmpty().WithMessage("Text is required")
                      .Length(6, 4096).WithMessage("Text should be between 6 and 4096 chars.");

        }
    }
}
