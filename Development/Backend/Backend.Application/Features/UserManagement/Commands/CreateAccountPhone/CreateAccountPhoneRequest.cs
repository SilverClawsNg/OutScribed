using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.CreateAccountPhone
{
    public class CreateAccountPhoneRequest
    {

        public string PhoneNumber { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
