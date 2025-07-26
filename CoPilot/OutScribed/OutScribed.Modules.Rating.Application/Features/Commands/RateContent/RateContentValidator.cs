using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Rating.Application.Features.Commands.RateContent
{
    public class RateContentValidator : Validator<RateContentRequest>
    {
        public RateContentValidator()
        {

            RuleFor(c => c.ContentId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Type)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Rate type is required.")
                       .NotEmpty().WithMessage("Rate type is required.")
                       .IsInEnum().WithMessage("Rate type is invalid.");

            RuleFor(c => c.Content)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Required parameter is missing.")
                      .NotEmpty().WithMessage("Required parameter is missing.")
                      .IsInEnum().WithMessage("Required parameter is missing.");
        }
    }
}
