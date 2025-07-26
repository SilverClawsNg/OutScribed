namespace OutScribed.Infrastructure.EmailGateway.ViewModels
{
    public class SendResendPasswordRecoveryOtpMailVM(string EmailAddress, int Otp)
    {
        public int Otp { get; } = Otp;

        public string EmailAddress { get; } = EmailAddress;

        public string Topic { get; } = "Password recovery token...";
    }
}
