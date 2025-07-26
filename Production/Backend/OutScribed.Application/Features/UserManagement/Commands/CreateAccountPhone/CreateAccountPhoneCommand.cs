using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.CreateAccountPhone
{
    public class CreateAccountPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
