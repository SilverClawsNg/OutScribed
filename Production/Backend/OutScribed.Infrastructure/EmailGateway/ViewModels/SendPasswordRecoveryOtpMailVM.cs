namespace OutScribed.Infrastructure.EmailGateway.ViewModels
{
    public record SendPasswordRecoveryOtpMailVM(string EmailAddress, int Otp)
    {
        public int Otp { get; } = Otp;

        public string EmailAddress { get; } = EmailAddress;

        public string Topic { get; } = "Password recovery token...";
    }
}
