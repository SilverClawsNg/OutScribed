namespace OutScribed.Application.Interfaces
{
    public interface IMailSender
    {
        Task SendPasswordRecoveryOtpMail(string emailAddress, int otp);

        Task SendResendPasswordRecoveryOtpMail(string emailAddress, int otp);

        Task SendVerificationOtpMail(string emailAddress, int otp);

        Task SendResendVerificationOtpMail(string emailAddress, int otp);
    }
}
