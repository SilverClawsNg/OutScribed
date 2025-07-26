using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.UserManagement.Commands.CreateAccountEmail
{
    public class CreateAccountEmailRequest
    {

        public string EmailAddress { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
