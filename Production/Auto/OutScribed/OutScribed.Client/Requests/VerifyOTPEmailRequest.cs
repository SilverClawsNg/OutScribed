namespace OutScribed.Client.Requests
{
    public class VerifyOTPEmailRequest
    {

        public int Otp { get; set; }

        public string EmailAddress { get; set; } = null!;
    }
}
