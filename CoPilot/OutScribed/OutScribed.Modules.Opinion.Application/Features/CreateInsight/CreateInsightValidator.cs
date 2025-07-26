using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.CreateInsight
{
    public class CreateInsightValidator : Validator<CreateInsightRequest>
    {
        public CreateInsightValidator()
        {

            RuleFor(c => c.TaleId)
                        .Cascade(CascadeMode.Stop)
                        .NotNull().WithMessage("Required parameter is missing.")
                        .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Title)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Title is required")
                       .NotEmpty().WithMessage("Title is required")
                       .Length(3, 128).WithMessage("Title should be between 3 and 128 chars.");

            RuleFor(c => c.Category)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Category is required.")
                      .NotEmpty().WithMessage("Category is required.")
                      .IsInEnum().WithMessage("Category is invalid.");
        }
    }
}
