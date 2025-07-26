using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail
{
    public class ResendRegistrationOTPEmailRequest
    {
        public string EmailAddress { get; set; } = null!;


    }
}
