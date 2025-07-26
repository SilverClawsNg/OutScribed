using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<Result<bool>>
    {

        public Guid AccountId { get; set; }

        public string OldPassword { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
