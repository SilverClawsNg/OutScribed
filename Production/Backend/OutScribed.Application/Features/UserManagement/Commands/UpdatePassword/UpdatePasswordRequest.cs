namespace OutScribed.Application.Features.UserManagement.Commands.UpdatePassword
{
    public class UpdatePasswordRequest
    {

        public string OldPassword { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
