using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SendRegistrationOTPEmail
{
    public class SendRegistrationOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;


    }
}
