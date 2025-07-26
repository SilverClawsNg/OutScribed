using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.SendRecoveryOTP
{
    public class SendRecoveryOTPCommand : IRequest<Result<bool>>
    {
        public string Username { get; set; } = null!;


    }
}
