namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPPhone
{
    public class VerifyOTPPhoneRequest
    {
        public string PhoneNumber { get; set; } = null!;

        public int Otp { get; set; }

    }
}
