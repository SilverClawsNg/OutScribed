using Backend.Domain.Enums;

namespace Backend.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPPhone
{
    public class ResendRegistrationOTPPhoneRequest
    {
        public string PhoneNumber { get; set; } = null!;


    }
}
