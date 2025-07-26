namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateProfile
{
    public record UpdateProfileResponse(string PhotoUrl)
    {

        public string PhotoUrl { get; set; } = PhotoUrl;

    }
}
