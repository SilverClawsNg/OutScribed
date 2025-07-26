namespace Backend.Application.Features.UserManagement.Commands.LogoutUser
{
    public class LogoutUserRequest
    {
        public string RefreshToken { get; set; } = default!;

    }
}
