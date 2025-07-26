using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.FollowUser
{
    public class FollowUserCommand : IRequest<Result<FollowUserResponse>>
    {
        public Guid UserId { get; set; }

        public Guid FollowerId { get; set; }

        public bool Option { get; set; }

    }
}
