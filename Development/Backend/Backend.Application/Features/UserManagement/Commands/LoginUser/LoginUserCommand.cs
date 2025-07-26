using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<LoginUserResponse>>
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
