namespace OutScribed.Client.Responses
{
    public class RefreshTokenResponse
    {
        public bool IsSuccessful { get; set; }

        public string Token { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;

    }
}
