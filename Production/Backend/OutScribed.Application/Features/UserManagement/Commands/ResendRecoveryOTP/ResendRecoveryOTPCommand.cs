using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.ResendRecoveryOTP
{
    public class ResendRecoveryOTPCommand : IRequest<Result<bool>>
    {
        public string Username { get; set; } = null!;


    }
}
