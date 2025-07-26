namespace OutScribed.Application.Features.UserManagement.Commands.RefreshToken
{
    public class RefreshTokenResponse
    {

        public bool IsSuccessful { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

    }
}
