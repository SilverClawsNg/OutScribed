namespace OutScribed.Modules.Onboarding.Application.Features.Commands.VerifyToken
{
    public class VerifyTokenRequest
    {

        public Ulid? Id { get; set; }

        public string? EmailAddress { get; set; }

        public string? Token { get; set; }
    }
}
