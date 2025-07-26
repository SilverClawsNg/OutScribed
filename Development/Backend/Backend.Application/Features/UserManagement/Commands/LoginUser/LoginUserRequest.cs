namespace Backend.Application.Features.UserManagement.Commands.LoginUser
{
    public class LoginUserRequest
    {

        public required string Username { get; set; }

        public required string Password { get; set; }

    }
}
