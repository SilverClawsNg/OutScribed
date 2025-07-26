using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail
{
    public class ResendRegistrationOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;


    }
}
