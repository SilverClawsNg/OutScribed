using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SendRegistrationOTPPhone
{
    public class SendRegistrationOTPPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;


    }
}
