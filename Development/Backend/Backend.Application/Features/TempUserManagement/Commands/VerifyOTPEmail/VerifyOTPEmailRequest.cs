namespace Backend.Application.Features.TempUserManagement.Commands.VerifyOTPEmail
{
    public class VerifyOTPEmailRequest
    {
        public string EmailAddress { get; set; } = null!;

        public int Otp { get; set; }

    }
}
