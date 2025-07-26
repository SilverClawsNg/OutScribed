using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPEmail
{
    public class SetDoNotResendOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;

    }
}
