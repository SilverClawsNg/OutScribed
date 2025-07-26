using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<Result<bool>>
    {

        public Guid AccountId { get; set; }

        public string OldPassword { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
