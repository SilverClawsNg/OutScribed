namespace OutScribed.Client.Requests
{
    public class VerifyOTPPhoneRequest
    {

        public int Otp { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }
}
