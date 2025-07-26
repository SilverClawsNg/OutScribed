using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Flagging.Application.Features.Commands.FlagContent
{
    public class FlagContentValidator : Validator<FlagContentRequest>
    {
        public FlagContentValidator()
        {

            RuleFor(c => c.ContentId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Type)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Flag type is required.")
                       .NotEmpty().WithMessage("Flag type is required.")
                       .IsInEnum().WithMessage("Flag type is invalid.");

            RuleFor(c => c.Content)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Required parameter is missing.")
                      .NotEmpty().WithMessage("Required parameter is missing.")
                      .IsInEnum().WithMessage("Required parameter is missing.");
        }
    }
}
