namespace OutScribed.Modules.Identity.Application.Features.Commands.CreateAccount
{
    public class CreateAccountRequest
    {
        public string? EmailAddress { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}
