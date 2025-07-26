namespace OutScribed.Modules.Identity.Application.Features.Commands.ResetPassword
{
    public class ResetPasswordRequest
    {
        public string? EmailAddress { get; set; }

        public int? Token { get; set; }

        public string? Password { get; set; }
    }
}
