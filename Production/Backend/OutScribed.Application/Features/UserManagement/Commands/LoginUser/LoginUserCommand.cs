using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<LoginUserResponse>>
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
