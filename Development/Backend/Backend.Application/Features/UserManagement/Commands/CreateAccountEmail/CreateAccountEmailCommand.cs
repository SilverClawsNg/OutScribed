using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.CreateAccountEmail
{
    public class CreateAccountEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
