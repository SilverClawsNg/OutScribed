using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPPhone
{
    public class ResendRegistrationOTPPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;


    }
}
