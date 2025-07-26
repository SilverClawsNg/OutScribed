using Backend.Domain.Enums;

namespace Backend.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail
{
    public class ResendRegistrationOTPEmailRequest
    {
        public string EmailAddress { get; set; } = null!;


    }
}
