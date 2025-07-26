namespace OutScribed.Infrastructure.EmailGateway.ViewModels
{
    public record SendVerificationOtpMailVM(string EmailAddress, int Otp)
    {
        public int Otp { get; } = Otp;

        public string EmailAddress { get; } = EmailAddress;

        public string Topic { get; } = "Registration verification token...";
    }
}
