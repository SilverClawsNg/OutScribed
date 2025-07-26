using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.SendRecoveryOTP
{
    public class SendRecoveryOTPCommand : IRequest<Result<bool>>
    {
        public string Username { get; set; } = null!;


    }
}
