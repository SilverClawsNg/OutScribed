using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Following.Application.Features.Commands.UnfollowContent
{
    public class UnfollowContentValidator : Validator<UnfollowContentRequest>
    {
        public UnfollowContentValidator()
        {

            RuleFor(c => c.ContentId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Content)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Required parameter is missing.")
                      .NotEmpty().WithMessage("Required parameter is missing.")
                      .IsInEnum().WithMessage("Required parameter is missing.");
        }
    }
}
