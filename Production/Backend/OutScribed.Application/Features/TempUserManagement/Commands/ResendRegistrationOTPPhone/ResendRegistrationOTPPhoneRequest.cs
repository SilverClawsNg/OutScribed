using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPPhone
{
    public class ResendRegistrationOTPPhoneRequest
    {
        public string PhoneNumber { get; set; } = null!;


    }
}
