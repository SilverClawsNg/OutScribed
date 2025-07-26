namespace OutScribed.Application.Features.UserManagement.Commands.ResetPassword
{
    public class ResetPasswordRequest
    {

        public string Username { get; set; } = null!;

        public int Otp { get; set; }

        public string Password { get; set; } = null!;

    }
}
