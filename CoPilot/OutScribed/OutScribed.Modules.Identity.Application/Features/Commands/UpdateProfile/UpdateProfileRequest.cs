namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileRequest
    {
        public string? Title { get; set; }

        public string? Bio { get; set; }

        public string? Base64String { get; set; }

        public string? EmailAddress { get; set; }

        public bool IsHidden { get; set; }
    }
}
