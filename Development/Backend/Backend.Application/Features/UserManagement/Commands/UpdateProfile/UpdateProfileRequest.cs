namespace Backend.Application.Features.UserManagement.Commands.UpdateProfile
{
    public class UpdateProfileRequest
    {

        public string Title { get; set; } = null!;

        public string Bio { get; set; } = null!;

        public string? Base64String { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public bool IsHidden { get; set; }

    }
}
