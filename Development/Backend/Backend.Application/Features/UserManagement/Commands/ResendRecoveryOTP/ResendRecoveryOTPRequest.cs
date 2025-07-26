using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.ResendRecoveryOTP
{
    public class ResendRecoveryOTPRequest
    {
        public string Username { get; set; } = null!;


    }
}
