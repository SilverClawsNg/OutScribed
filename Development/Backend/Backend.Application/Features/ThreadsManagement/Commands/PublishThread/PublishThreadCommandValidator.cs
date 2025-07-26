using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.PublishThread;

public class PublishThreadCommandValidator : AbstractValidator<PublishThreadCommand>
{
    public PublishThreadCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");


    }
}
