using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result<bool>>
    {

        public string Username { get; set; } = null!;

        public int Otp { get; set; }

        public string Password { get; set; } = null!;

    }
}
