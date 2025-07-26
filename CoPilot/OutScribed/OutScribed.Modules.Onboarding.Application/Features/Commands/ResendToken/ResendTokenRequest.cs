namespace OutScribed.Modules.Onboarding.Application.Features.Commands.ResendToken
{
    public class ResendTokenRequest
    {

        public Ulid? Id { get; set; }

        public string? EmailAddress { get; set; }

        public string? NewEmailAddress { get; set; } // Optional: If user wants to change email

    }
}
