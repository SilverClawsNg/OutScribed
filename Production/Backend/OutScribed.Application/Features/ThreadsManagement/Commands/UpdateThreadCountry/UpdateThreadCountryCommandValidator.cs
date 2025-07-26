using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadCountry;

public class UpdateThreadCountryCommandValidator : AbstractValidator<UpdateThreadCountryCommand>
{
    public UpdateThreadCountryCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Draft is required.")
      .NotEmpty().WithMessage("Draft is required.");

        RuleFor(c => c.Country)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Country is required.")
          .NotEmpty().WithMessage("Country is required.")
          .IsInEnum().WithMessage("Invalid country.");

    }
}
