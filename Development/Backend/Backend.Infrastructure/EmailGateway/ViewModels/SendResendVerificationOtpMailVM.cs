namespace Backend.Infrastructure.EmailGateway.ViewModels
{
    public class SendResendVerificationOtpMailVM(string EmailAddress, int Otp)
    {
        public int Otp { get; } = Otp;

        public string EmailAddress { get; } = EmailAddress;

        public string Topic { get; } = "Registration verification token...";
    }
}
