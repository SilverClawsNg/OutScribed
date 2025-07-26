using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.FollowUser
{
    public class FollowUserCommand : IRequest<Result<FollowUserResponse>>
    {
        public Guid UserId { get; set; }

        public Guid FollowerId { get; set; }

        public bool Option { get; set; }

    }
}
