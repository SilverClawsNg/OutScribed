using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.ResendRecoveryOTP
{
    public class ResendRecoveryOTPCommand : IRequest<Result<bool>>
    {
        public string Username { get; set; } = null!;


    }
}
