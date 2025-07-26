namespace OutScribed.Client.Requests
{
    public class LogoutUserRequest
    {
        public string? RefreshToken { get; set; } = default!;
    }
}
