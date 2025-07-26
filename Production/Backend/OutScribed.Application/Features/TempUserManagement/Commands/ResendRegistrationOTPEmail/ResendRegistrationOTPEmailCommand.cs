using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail
{
    public class ResendRegistrationOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;


    }
}
